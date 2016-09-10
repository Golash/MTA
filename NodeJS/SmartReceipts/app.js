var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
var logger = require('morgan');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');
var mongodb = require("mongodb");
var ObjectID = mongodb.ObjectID;
var REQUESTS_COLLECTION = "requests";


var app = express();
app.use(express.static(path.join(__dirname, 'public')));
app.use(bodyParser.json());
// uncomment after placing your favicon in /public
//app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.use(logger('dev'));
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());


// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');

var MONGODB_URI = 'mongodb://localhost:27017/receipts';

// Create a database variable outside of the database connection callback to reuse the connection pool in your app.
var db;
// Connect to the database before starting the application server.
mongodb.MongoClient.connect(MONGODB_URI, function (err, database) {
    if (err) {
        console.log(err);
        process.exit(1);
    }

    // Save database object from the callback for reuse.
    db = database;
    console.log("Database connection ready");

    // Initialize the app.
    var server = app.listen(process.env.PORT || 8080, function () {
        var port = server.address().port;
        console.log("App now running on port", port);
    });
});

var createDBRequest = function (req) {
    var newRequest = {};
    newRequest.Uri = req.originalUrl;
    newRequest.createTime = new Date();
    newRequest.RequestedBy = req.body.RequestedBy;
    newRequest.IsValid = true;
    newRequest.ErrorReason = "";

    return newRequest;

};
// Make our db accessible to our router
app.use(function(req,res,next){
    req.db = db;

    // Save request to db:
    var newRequest = createDBRequest(req);
    db.collection(REQUESTS_COLLECTION).insertOne(newRequest, function(err, doc) {
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
