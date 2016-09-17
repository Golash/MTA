/**
 * Created by t-nawol on 14-Sep-16.
 */

var receiptTotalPrice = 0;
var customersTable;
var businessTable;
var startDateCustomers;
var endDateCustomers;
var startDateBusiness;
var endDateBusiness;

var statisticsUrlsTable;
var statisticsUsersTable;
var startDateStatistics;
var endDateStatistics;

$( document ).ready(function() {
    customersTable = $('#customers-table').DataTable({
        columns: [
            { data: 'receipt_id' },
            { data: 'dateTime' },
            { data: 'business_name' },
            { data: 'category' },
            { data: 'total_price' }
        ]
    });

    businessTable = $('#business-table').DataTable({
        columns: [
            { data: 'receipt_id' },
            { data: 'dateTime' },
            { data: 'customer_name' },
            { data: 'total_price' }
        ]
    });


    // Init Customers
    startDateCustomers = $( "#customers-query-start-date" ).datepicker({
        dateFormat: 'dd/mm/yy'
    });
    endDateCustomers = $( "#customers-query-end-date" ).datepicker({
        dateFormat: 'dd/mm/yy'
    });

    startDateCustomers.datepicker('setDate', moment().subtract(1, 'd').toDate());
    endDateCustomers.datepicker('setDate', moment().toDate());

    // Init Business
    startDateBusiness = $( "#business-query-start-date" ).datepicker({
        dateFormat: 'dd/mm/yy'
    });
    endDateBusiness = $( "#business-query-end-date" ).datepicker({
        dateFormat: 'dd/mm/yy'
    });

    startDateBusiness.datepicker('setDate', moment().subtract(1, 'd').toDate());
    endDateBusiness.datepicker('setDate', moment().toDate());

    // Init Statistics for Urls
    statisticsUrlsTable = $('#statistics-urls-table').DataTable({
        columns: [
            { data: 'url' },
            { data: 'request_count' }
        ]
    });

    statisticsUsersTable = $('#statistics-users-table').DataTable({
        columns: [
            { data: 'url' },
            { data: 'users_count' }
        ]
    });

    startDateStatistics = $( "#statistics-query-start-date" ).datepicker({
        dateFormat: 'dd/mm/yy'
    });
    endDateStatistics = $( "#statistics-query-end-date" ).datepicker({
        dateFormat: 'dd/mm/yy'
    });

    startDateStatistics.datepicker('setDate', moment().subtract(1, 'd').toDate());
    endDateStatistics.datepicker('setDate', moment().toDate());

});



function tabButtonClicked(btn){
    var tabId = btn.id.replace("btn","tab");
    $(".tab-view").each(function() {
        $( this ).addClass("tab-view-inactive")
    });
    $("#"+tabId).removeClass("tab-view-inactive");
}

function clean() {
    $('#tab-new-receipt').find('input:text').val('');

    document.getElementById("Price").value = "";
    document.getElementById("Amount").value = "";

    var table = document.getElementById("ProductsTable-id");
    while(table.rows.length > 0) {
        table.deleteRow(0);
    }

    document.getElementById("receiptTotalPrice").innerHTML = "0";
}

function addProduct() {
    var table = document.getElementById("ProductsTable-id");
    var row = table.insertRow();

    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    var cell4 = row.insertCell(3);

    var product = $("#Product").val();
    var price = $("#Price").val();
    var amount = $("#Amount").val();
    var totalPrice = price * amount;

    cell1.width = "25%";
    cell2.width = "25%";
    cell3.width = "25%";
    cell4.width = "25%";

    if (product == "") {
        alert("You have to chose a product");
        return;
    }

    if (price == "") {
        alert("You have to insert a price");
        return;
    }

    if (amount == "") {
        alert("You have to insert amount");
        return;
    }

    cell1.innerHTML = product;
    cell2.innerHTML = price;
    cell3.innerHTML = amount;
    cell4.innerHTML = totalPrice.toString();

    receiptTotalPrice += totalPrice;
    document.getElementById("receiptTotalPrice").innerHTML = receiptTotalPrice.toString();
}


// Customers Tab
function btnCustomersSearchClicked() {
    var startDate = moment(startDateCustomers.datepicker("getDate")).format("YYYY-MM-DD");
    var endDate = moment(endDateCustomers.datepicker("getDate")).format("YYYY-MM-DD")+"T23:59:59.999";
    var query = {
        RequestedBy : $("#"+"query-customer-id").val(),
        StartDate : startDate,
        EndDate : endDate,
        FilterType: "",
        FilterValue: ""
    };

    if (!query.RequestedBy) {
        RequestedBy = "Admin"
    };

    setCustomerReceiptsGridAsync(query);
}

function setCustomerReceiptsGridAsync(query) {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: "/api/receipts/customer/preview",
        data: query,
        async: true,
        success: function (data) {
            var rows = convertCustomersDBDataToGridFormat(data);
            customersTable.clear();
            customersTable.rows.add(rows);
            customersTable.draw();
        },
        error: function (err) {
            alert("Failed!" + err);
        }
    });
}

function convertCustomersDBDataToGridFormat(data) {
    var rows = [];
    for (var i = 0; i < data.length; i++) {
        var row = data[i];
        rows.push({
            "receipt_id": row._id,
            "dateTime": row.DateTime,
            "business_name": row.Business.Name,
            "category": row.Business.Category,
            "total_price": row.TotalPrice
        });
    }

    return rows;
}

// Business Tab
function btnBusinessSearchClicked() {
    var startDate = moment(startDateBusiness.datepicker("getDate")).format("YYYY-MM-DD");
    var endDate = moment(endDateBusiness.datepicker("getDate")).format("YYYY-MM-DD")+"T23:59:59.999";
    var query = {
        RequestedBy : $("#"+"query-business-id").val(),
        StartDate : startDate,
        EndDate : endDate,
        FilterType: "",
        FilterValue: ""
    };

    if (!query.RequestedBy) {
        RequestedBy = "Admin";
    }
    setBusinessReceiptsGridAsync(query);
}

function setBusinessReceiptsGridAsync(query) {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: "/api/receipts/business/preview",
        data: query,
        async: true,
        success: function (data) {
            var rows = convertBusinessDBDataToGridFormat(data);
            businessTable.clear();
            businessTable.rows.add(rows);
            businessTable.draw();
        },
        error: function (err) {
            alert("Failed!" + err);
        }
    });
}

function convertBusinessDBDataToGridFormat(data) {
    var rows = [];
    for (var i = 0; i < data.length; i++) {
        var row = data[i];
        rows.push({
            "receipt_id": row._id,
            "dateTime": row.DateTime,
            "customer_name": row.Customer.Name,
            "total_price": row.TotalPrice
        });
    }

    return rows;
}

function sendReceipt() {
    var customerName = $("#customer-name").val();
    var customerId = $("#customer-id").val();
    var BusinessName = $("#Business").val();
    var BusinessID = $("#Business-id").val();
    var Category = $("#Category").val();
    var LastFourNumbers = $("#LastFourNumbers").val();
    var Product = $("#Product").val();
    var receiptPrice = $("#receiptTotalPrice").val();


    var data = {
        RequestedBy : BusinessID,
        Customer: {
            Id: customerId,
            Name: customerName
        },
        Business: {
            Id: BusinessID,
            Name: BusinessName,
            Category: Category
        },
        CreditCard: {
            LastFourNumbers: LastFourNumbers
        },
        Products: [],

        TotalPrice: receiptPrice
    };

    if (!data.RequestedBy) {
        alert("please fill all the fields");
        return;
    }

    var table = document.getElementById("ProductsTable-id");
    for (var i = 0, row; row = table.rows[i]; i++) {
        data.Products.push(
            {
                Name : row.cells[0].innerText,
                Price : row.cells[1].innerText,
                Amount : row.cells[2].innerText,
                TotalPrice : row.cells[3].innerText
            }
        );
    }

    data.TotalPrice = receiptTotalPrice;

    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: "/api/receipts/send",
        data: data,
        async: false,
        success: function (data) {
            clean();
            alert("Sent!");
        },
        error: function (err) {
            alert("Failed!" + err);
        }
    });
}

function getNewReceiptObject() {
    return {
        "RequestedBy" : "",
        "Customer": {
            "Id": "",
            "Name": ""
        },
        "Business": {
            "Id": "",
            "Name": "",
            "Category": ""
        },
        "CreditCard": {
            "LastFourNumbers": ""
        },
        "Products": [],

        "TotalPrice": ""
    }
}
function btnGetStatisticsClicked() {
    btnStatisticsSearchClicked();
}

function btnStatisticsSearchClicked () {
    var requestedBy = "Admin";
    var startDate = moment(startDateStatistics.datepicker("getDate")).format("YYYY-MM-DD");
    var endDate = moment(endDateStatistics.datepicker("getDate")).format("YYYY-MM-DD")+"T23:59:59.999";

    fillUsersStatistics(requestedBy, startDate, endDate);
    fillUrlsStatistics(requestedBy, startDate, endDate);
}

function fillUsersStatistics(requestedBy, startDate, endDate) {
    var urls = [
        "/api/receipts/send",
        "/api/receipts/customer/preview",
        "/api/receipts/business/preview",
        "/api/receipts/details",
        "/api/receipts/statistics/users/count",
        "/api/receipts/statistics/requests/count"
    ];

    var rows = [];
    for (var i=0; i<urls.length; i++){
        var uri = urls[i];
        var query = {
            "RequestedBy": requestedBy,
            "StartDate" : startDate,
            "EndDate" : endDate,
            "Uri": uri
        };
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: "/api/receipts/statistics/users/count",
            data: query,
            async: false,
            success: function (count) {
                rows.push({
                    url: uri,
                    users_count : count
                });
            },
            error: function (err) {
                alert("Failed!" + err);
            }
        });

        statisticsUsersTable.clear();
        statisticsUsersTable.rows.add(rows);
        statisticsUsersTable.draw();
    }
}

function fillUrlsStatistics(requestedBy, startDate, endDate) {
    var urls = [
        "/api/receipts/send",
        "/api/receipts/customer/preview",
        "/api/receipts/business/preview",
        "/api/receipts/details",
        "/api/receipts/statistics/users/count",
        "/api/receipts/statistics/requests/count"
    ];

    var rows = [];
    for (var i=0; i<urls.length; i++){
        var uri = urls[i];
        var query = {
            "RequestedBy": requestedBy,
            "StartDate" : startDate,
            "EndDate" : endDate,
            "Uri": uri
        };
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: "/api/receipts/statistics/requests/count",
            data: query,
            async: false,
            success: function (count) {
                rows.push({
                    url: uri,
                    request_count : count
                });
            },
            error: function (err) {
                alert("Failed!" + err);
            }
        });

        statisticsUrlsTable.clear();
        statisticsUrlsTable.rows.add(rows);
        statisticsUrlsTable.draw();
    }
}