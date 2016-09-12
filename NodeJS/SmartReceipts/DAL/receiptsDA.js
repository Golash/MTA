var exports = module.exports = {};
var mongoConnection = require("./mongoConnection");
var RECEIPTS_COLLECTION = "receipts";

exports.insert = function (receipt, callbacks) {
    mongoConnection.db.collection(RECEIPTS_COLLECTION).insertOne(receipt, function(err, doc) {
        if (err) {
            callbacks.onFailed(err);
        } else {
            callbacks.onSuccess(doc);
        }
    });
};

exports.getCustomerPreview = function (clientID, startDate, endDate, filterType, filterValue, callbacks) {
    // Set the fields that need bo be displayed
    var previewFields = { _id: 1, Business: 1, TotalPrice : 1, DateTime : 1 };

    // Build the query for customer preview
    var query = {
        "DateTime" : { $gte : new Date(startDate),  $lte : new Date(endDate)},
        "Customer.Id": clientID
    };

    if (filterType && filterValue) {
        query["Business."+filterType] = filterValue;
    }

    queryPreview(query, previewFields, callbacks);
};

exports.getBusinessPreview = function (clientID, startDate, endDate, filterType, filterValue, callbacks) {
    // Set the fields that need bo be displayed
    var previewFields = { _id: 1, Customer: 1, TotalPrice : 1, DateTime : 1 };

    // Build the query for business preview
    var query = {
        "DateTime" : { $gte : new Date(startDate),  $lte : new Date(endDate)},
        "Business.Id": clientID
    };

    if (filterType && filterValue) {
        query["Customer."+filterType] = filterValue;
    }

    queryPreview(query, previewFields, callbacks);
};

function queryPreview(query, previewFields, callbacks) {
    // Run the query and call the callback as needed.
    mongoConnection.db.collection(RECEIPTS_COLLECTION).find(query, previewFields).toArray(function(err, docs) {
        if (err) {
            callbacks.onFailed(err);
        } else {
            callbacks.onSuccess(docs);
        }
    });
}

exports.getReceiptDetails = function(receiptId, callbacks) {
    var query = {
        _id : new mongoConnection.ObjectID(receiptId)
    };
    console.log(query);
    mongoConnection.db.collection(RECEIPTS_COLLECTION).findOne(query, function(err, doc) {
        if (err) {
            callbacks.onFailed(err);
        } else {
            callbacks.onSuccess(doc);
        }
    });
};
