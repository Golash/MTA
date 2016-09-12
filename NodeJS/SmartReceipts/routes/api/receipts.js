var express = require('express');
var receiptsDA = require('../../DAL//receiptsDA');
var requestsDA = require('../../DAL//requestsDA');
var router = express.Router();
var mongodb = require("mongodb");

// Generic error handler used by all endpoints.
function handleError(res, reason, message, code) {
    console.log("ERROR: " + reason);
    res.status(code || 500).json({"error": message});
}

router.route('/send').post(function(req, res, next) {
    var newReceipt = req.body;
    newReceipt.DateTime = new Date();
    receiptsDA.insert(newReceipt,{
        onSuccess: function(doc){
            res.status(201).json(doc.ops[0]);
        },
        onFailed: function (err) {
            handleError(res, "Failed to insert receipt", err.message);
        }
    });
});

router.route('/customer/preview').post(function(req, res, next) {
    var clientID = req.body.RequestedBy;
    var startDate = req.body.StartDate;
    var endDate = req.body.EndDate;
    var filterType = req.body.FilterType;
    var filterValue = req.body.FilterValue;

    receiptsDA.getCustomerPreview(clientID, startDate, endDate, filterType, filterValue, {
        onSuccess: function(docs){
            res.status(200).json(docs);
        },
        onFailed: function (err) {
            handleError(res, "Failed to get customer preview", err.message);
        }
    });
});

router.route('/business/preview').post(function(req, res, next) {
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

    receiptsDA.getBusinessPreview(clientID, startDate, endDate, filterType, filterValue, {
        onSuccess: function(docs){
            res.status(200).json(docs);
        },
        onFailed: function (err) {
            handleError(res, "Failed to get business preview", err.message);
        }
    });
});

router.route('/details').post(function(req, res, next) {
    var receiptID = req.body.ReceiptId;

    receiptsDA.getReceiptDetails(receiptID, {
        onSuccess: function(doc){
            res.status(200).json(doc);
        },
        onFailed: function (err) {
            handleError(res, "Failed to get details for receipt id: "+receiptID, err.message);
        }
    });
});

router.route('/statistics/users/count').post(function(req, res, next) {
    var startDate = req.body.StartDate;
    var endDate = req.body.EndDate;
    var uri = req.body.Uri;

    requestsDA.getUsersCountStatistics(startDate, endDate, uri, {
        onSuccess: function(userCount){
            res.status(200).json(userCount);
        },
        onFailed: function (err) {
            handleError(res, "Failed to get users count statistics", err.message);
        }
    })
});

router.route('/statistics/requests/count').post(function(req, res, next) {
    var startDate = req.body.StartDate;
    var endDate = req.body.EndDate;
    var uri = req.body.Uri;

    requestsDA.getRequestsCountStatistics(startDate, endDate, uri, {
        onSuccess: function(requestsCount){
            res.status(200).json(requestsCount);
        },
        onFailed: function (err) {
            handleError(res, "Failed to get requests count statistics", err.message);
        }
    })
});

module.exports = router;
