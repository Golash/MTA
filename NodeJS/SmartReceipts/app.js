var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
var logger = require('morgan');
var cookieParser = require('cookie-parser');
var mongoConnection = require("./DAL/mongoConnection");
var bodyParser = require('body-parser');
var REQUESTS_COLLECTION = "requests";

var app = express();
app.use(express.static(path.join(__dirname, 'public')));
app.use(bodyParser.json());
// uncomment after placing your favicon in /public
//app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.use(logger('dev'));
app.use(bodyParser.urlencoded({ extended: true }));
app.use(cookieParser());


// view engine setup
var engine = require('consolidate');
app.set('views', path.join(__dirname, 'views'));
app.engine('html', engine.mustache);
app.set('view engine', 'html');

var onMongoConnectSuccess = function(db) {
    console.log("Database connection ready");

    // After connected successfully to the DB Initialize the app.
    var server = app.listen(process.env.PORT || 8080, function () {
        var port = server.address().port;
        console.log("App now running on port", port);
    });
};

var onMongoConnectFailed = function (err) {
    console.log(err);
    process.exit(1);
};

// Connect to MongoDB
mongoConnection.connect(onMongoConnectSuccess, onMongoConnectFailed);

var createDBRequest = function (req) {
    var newRequest = {};
    newRequest.Uri = req.originalUrl;
    newRequest.DateTime = new Date();
    newRequest.RequestedBy = req.body.RequestedBy;

    var errorReason = "";
    var isValid = true;
    if (!newRequest.RequestedBy) {
        isValid = false;
        errorReason = "'RequestedBy' field can't be empty";
    }

    newRequest.IsValid = isValid;
    newRequest.ErrorReason = errorReason;

    return newRequest;

};
// Make our db accessible to our router
app.use("/api",function(req,res,next){
    // Save request to db:
    var newRequest = createDBRequest(req);

    if (!newRequest.IsValid) {
        var err = new Error('Invalid Request, Reason: ' + newRequest.ErrorReason);
        err.status = 500;
        res.status = 500;
        next(err);
    }

    mongoConnection.db.collection(REQUESTS_COLLECTION).insertOne(newRequest, function(err, doc) {
        if (err) {
            console.log("Error: "+ err.message);
        }
    });

    next();
});

var routes = require('./routes/index');
var users = require('./routes/users');
var receipts = require('./routes/api/receipts');

app.use('/', routes);
app.use('/users', users);
app.use('/api/receipts', receipts);

// catch 404 and forward to error handler
app.use(function(req, res, next) {
  var err = new Error('Not Found');
  err.status = 404;
  next(err);
});

// error handlers

// development error handler
// will print stacktrace
if (app.get('env') === 'development') {
  app.use(function(err, req, res, next) {
    res.status(err.status || 500);
    res.render('error', {
      message: err.message,
      error: err
    });
  });
}

// production error handler
// no stacktraces leaked to user
app.use(function(err, req, res, next) {
  res.status(err.status || 500);
  res.render('error', {
    message: err.message,
    error: {}
  });
});


module.exports = app;
