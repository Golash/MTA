var express = require('express');
var router = express.Router();
var mongodb = require("mongodb");
var ObjectID = mongodb.ObjectID;
var RECEIPTS_COLLECTION = "receipts";
var REQUEST_COLLECTION = "requests";

// Generic error handler used by all endpoints.
function handleError(res, reason, message, code) {
    console.log("ERROR: " + reason);
    res.status(code || 500).json({"error": message});
}

router.route('/send').post(function(req, res, next) {
    var db = req.db;
    var newReceipt = req.body;
    newReceipt.DateTime = new Date();


    db.collection(RECEIPTS_COLLECTION).insertOne(newReceipt, function(err, doc) {
        if (err) {
            handleError(res, err.message, "Failed to create new receipts.");
        } else {
            res.status(201).json(doc.ops[0]);
        }
    });
});

router.route('/customer/preview').post(function(req, res, next) {
    var db = req.db;
    var clientID = req.body.RequestedBy;
    var startDate = req.body.StartDate;
    var endDate = req.body.EndDate;
    var filterType = req.body.FilterType;
    var filterValue = req.body.FilterValue;

    var query = {
        "DateTime" : { $gte : new Date(startDate),  $lte : new Date(endDate)},
        "Customer.Id": clientID
    };

    if (filterType && filterValue) {
        query["Business."+filterType] = filterValue;
    }


    console.log(query);
    selectFildes = { _id: 1, Business: 1, TotalPrice : 1, DateTime : 1 };

    console.log(query);
    db.collection(RECEIPTS_COLLECTION).find(query, selectFildes).toArray(function(err, docs) {
        if (err) {
            handleError(res, err.message, "Failed to get contacts.");
        } else {
            res.status(200).json(docs);
        }
    });
});

router.route('/business/preview').post(function(req, res, next) {
    var db = req.db;
    var clientID = req.body.RequestedBy;
    var startDate = req.body.StartDate;
    var endDate = req.body.EndDate;
    var filterType = req.body.FilterType;
    var filterValue = req.body.FilterValue;

    var query = {
        "DateTime" : { $gte : new Date(startDate),  $lte : new Date(endDate)},
        "Business.Id": clientID
    };

    if (filterType && filterValue) {
        query["Customer."+filterType] = filterValue;
    }

    selectFildes = { _id: 1, Customer: 1, TotalPrice : 1, DateTime : 1 };
    console.log(query);
    db.collection(RECEIPTS_COLLECTION).find(query, selectFildes).toArray(function(err, docs) {
        if (err) {
            handleError(res, err.message, "Failed to get contacts.");
        } else {
            res.status(200).json(docs);
        }
    });
});

router.route('/details').post(function(req, res, next) {
    var db = req.db;
    var receiptID = req.body.ReceiptId;

    var query = {
        _id : new ObjectID(receiptID)
    };
    console.log(query);
    db.collection(RECEIPTS_COLLECTION).findOne(query, function(err, doc) {
        if (err) {
            handleError(res, err.message, "Failed to get contacts.");
        } else {
            res.status(200).json(doc);
        }
    });
});

router.route('/statistics/requests/count').post(function(req, res, next) {
    var db = req.db;
    var startDate = req.body.StartDate;
    var endDate = req.body.EndDate;
    var uri = req.body.Uri;

    var query = {
        "DateTime" : { $gte : new Date(startDate),  $lte : new Date(endDate)}
    };

    if (uri) {
        query.Uri = uri;
    }
    console.log(query);
    db.collection(REQUEST_COLLECTION).find(query).toArray(function(err, docs) {
        if (err) {
            handleError(res, err.message, "Failed to get contacts.");
        } else {
            res.status(200).json(docs.length);
        }
    });
});

router.route('/statistics/users/count').post(function(req, res, next) {
    var db = req.db;
    var startDate = req.body.StartDate;
    var endDate = req.body.EndDate;
    var uri = req.body.Uri;

    var query = {
        "DateTime" : { $gte : new Date(startDate),  $lte : new Date(endDate)},
        'RequestedBy' : { $ne: null }
    };

    if (uri) {
        query.Uri = uri;
    }
    console.log(query);
    db.collection(REQUEST_COLLECTION).distinct("RequestedBy",query, function(err, docs) {
        if (err) {
            handleError(res, err.message, "Failed to get contacts.");
        } else {
            res.status(200).json(docs.length);
        }
    });
});

module.exports = router;
    