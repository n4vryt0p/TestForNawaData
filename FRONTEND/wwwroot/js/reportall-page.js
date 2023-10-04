"use strict"
var dataGrid,$dcs = [{
    text: 'D',
}, {
    text: 'C',
},
];
$(document).ready(function () {
    var antiForgeryToken = document.getElementsByName("__RequestVerificationToken")[0].value;
    dataGrid = $('#roleGrid').dxDataGrid({
        dataSource: DevExpress.data.AspNet.createStore({
            key: 'id',
            loadUrl: `/Report/Read`,
            onBeforeSend(method, ajaxOptions) {
                if (antiForgeryToken) {
                    ajaxOptions.headers = {
                        "RequestVerificationToken": antiForgeryToken
                    };
                };
            },
        }),
        rowAlternationEnabled: true,
        searchPanel: {
            visible: false
        },
        paging: {
            pageSize: 10,
        },
        pager: {
            visible: true,
            allowedPageSizes: [5, 10, 'all'],
            showPageSizeSelector: true,
            showInfo: true,
            showNavigationButtons: true,
        },
        columns: [
            {
                dataField: 'accountId',
                visible: false
            },
            {
                dataField: 'transactionDate',
                caption: 'Transaction Date',
                dataType: 'date',
            },
            {
                dataField: 'masterTransactionId',
                caption: 'Description',
                allowSorting: false,
                allowFiltering: false,
                lookup: {
                    dataSource: window.$masters,
                    valueExpr: 'id',
                    displayExpr: 'text',
                }
            },
            {
                dataField: 'debitCreditStatus',
                caption: 'Credit',
                calculateCellValue(data) {
                    if (data.debitCreditStatus === 'D') {
                        return '-';
                    }
                    return data.amount;
                },
            },
            {
                dataField: 'debitCreditStatus',
                caption: 'Debit',
                calculateCellValue(data) {
                    if (data.debitCreditStatus === 'C') {
                        return '-';
                    }
                    return data.amount;
                },
            },
            {
                dataField: 'amount',
                caption: 'Amount',
            }
        ],
    }).dxDataGrid("instance");

    $('#selectStatus').dxSelectBox({
        dataSource: window.$users,
        value: window.$users[0],
        valueExpr: 'id',
        displayExpr: 'text',
        inputAttr: { 'aria-label': 'Users' },
        onValueChanged(data) {
            if (data.value === 0) { dataGrid.clearFilter(); } else { dataGrid.filter(['accountId', '=', data.value]); }
        },
    });

    $('#apply-value').dxDateRangeBox({
        applyValueMode: 'useButtons',
        onValueChanged(data) {
            /*if (data.value === 'All') { dataGrid.clearFilter(); } else { dataGrid.filter(['Task_Status', '=', data.value]); }*/
            if (data.value[0] == null || data.value[1] == null) {
                return;
            } else {
                dataGrid.filter([
                    ["transactionDate", ">=", data.value[0]],
                    "and",
                    ["transactionDate", "<=", data.value[1]]
                ]);
            }
        },
    });
});