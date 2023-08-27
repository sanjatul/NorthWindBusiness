

$(document).ready(function () {
    $('#tblData').DataTable({
        serverSide: true,
        ajax: {
            url: '/Home/GetAll', // Replace with your API endpoint URL
            type: 'GET', // You might use 'POST' depending on your API setup
            data: function (data) {
                // Pass necessary parameters to the API
                data.start = data.start;
                data.length = data.length;
                data.searchValue = data.search.value;
            },
            dataType: 'json',
            beforeSend: function (xhr) {
                // Set any headers if needed
            },
            success: function (response) {
                
                // Process the response data and populate the DataTable
                if (response && response.data) {
                    debugger
                    $('#tblData').DataTable().clear().rows.add(response.data).draw();
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                // Handle errors
            }
        },
        columns: [
            { data: 'orderId' },
            { data: 'customerName' },
            { data: 'productName' },
            { data: 'orderDate' },
            { data: 'quantity' },
            { data: 'price' },
            { data: 'amount' }
        ]
    });
});
