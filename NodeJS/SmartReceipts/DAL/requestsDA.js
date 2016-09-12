var exports = module.exports = {};
var mongoConnection = require("./mongoConnection");
var REQUEST_COLLECTION = "requests";

exports.getUsersCountStatistics = function (startDate, endDate, uri, callbacks) {
    var query = {
        "DateTime" : { $gte : new Date(startDate),  $lte : new Date(endDate)},
        'RequestedBy' : { $ne: null }
    };

    if (uri) {
        query.Uri = uri;
    }

    mongoConnection.db.collection(REQUEST_COLLECTION).distinct("RequestedBy", query, function(err, docs) {
        if (err) {
            callbacks.onFailed(err);
        } else {
            callbacks.onSuccess(docs.length);
        }
    });
};

exports.getRequestsCountStatistics = function (startDate, endDate, uri, callbacks) {
    var query = {
        "DateTime" : { $gte : new Date(startDate),  $lte : new Date(endDate)}
    };

    if (uri) {
        query.Uri = uri;
    }
    mongoConnection.db.collection(REQUEST_COLLECTION).find(query).toArray(function(err, docs) {
        if (err) {
            callbacks.onFailed(err);
        } else {
            callbacks.onSuccess(docs.length);
        }
    });
};