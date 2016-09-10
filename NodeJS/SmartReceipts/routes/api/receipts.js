var express = require('express');
var router = express.Router();
var RECEIPTS_COLLECTION = "receipts";

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
    var clientID = req.body.ClientID;
    var startDate = req.body.StartDate;
    var endDate = req.body.EndDate;
    var filterType = req.body.FilterType;
    var filterValue = req.body.FilterValue;

    var query = {
        "DateTime" : { $gte : new Date(startDate),  $lte : new Date(endDate)},
        "Customer.Id": clientID
    };
    query["Business."+filterType] = filterValue;

    console.log(query);
    selectFildes = { _id: 1, Business: 1, TotalPrice : 1, DateTime : 1 };

    db.collection(RECEIPTS_COLLECTION).find(query, selectFildes).toArray(function(err, docs) {
        if (err) {
            handleError(res, err.message, "Failed to get contacts.");
        } else {
            res.status(200).json(docs);
        }
    });
});
module.exports = router;
