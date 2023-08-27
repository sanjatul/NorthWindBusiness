//For Buyers table
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/home/getall' },
        "columns": [
            { data: 'orderId' },
            { data: 'customerName' },
            { data: 'productName' },
            { data: 'orderDate' },
            { data: 'quantity' },
            { data: 'price' },
            { data: 'amount' }
        ]
    });
}