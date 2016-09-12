var exports = module.exports = {};
var mongodb = require("mongodb");
var MONGODB_URI = 'mongodb://localhost:27017/receipts';

// Connect to the 'receipts' database and keep the connection in db global variable
exports.connect = function(onSuccessCallback, onFailedCallback) {
    // Connect to the database before starting the application server.
    mongodb.MongoClient.connect(MONGODB_URI, function (err, database) {
        if (err) {
            onFailedCallback(err);
        }

        // Save database object from the callback for reuse.
        exports.db = database;
        onSuccessCallback(exports.db);
    });
};
exports.ObjectID = mongodb.ObjectID;

