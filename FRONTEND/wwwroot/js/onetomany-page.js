"use strict"
var gridInstance,$dcs = [{
    text: 'D',
}, {
    text: 'C',
},
];
$(document).ready(function () {
    var antiForgeryToken = document.getElementsByName("__RequestVerificationToken")[0].value;
    gridInstance = $('#roleGrid').dxDataGrid({
        dataSource: DevExpress.data.AspNet.createStore({
            key: 'id',
            loadUrl: `/Transaction/Read`,
            insertUrl: `/Transaction/Create`,
            updateUrl: `/Transaction/Edit`,
            deleteUrl: `/Transaction/Delete`,
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
                caption: 'Account',
                allowSorting: false,
                allowFiltering: false,
                lookup: {
                    dataSource: window.$users,
                    valueExpr: 'id',
                    displayExpr: 'text',
                }
            },
            {
                dataField: 'transactionDate',
                caption: 'Transaction Date',
                dataType: 'date',
                allowEditing: false,
                allowSorting: false,
                allowFiltering: false,
                format: 'yyyy-MM-dd',
                validationRules: [{
                    type: 'required',
                    message: 'Transaction Date is required.',
                }]
            },
            {
                dataField: 'masterTransactionId',
                caption: 'Description',
                validationRules: [{
                    type: 'required',
                    message: 'Description is required.',
                }],
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
                caption: 'Debit/Credit',
                validationRules: [{
                    type: 'required',
                    message: 'Description is required.',
                }],
                allowSorting: false,
                allowFiltering: false,
                lookup: {
                    dataSource: window.$dcs,
                    valueExpr: 'text',
                    displayExpr: 'text',
                }
            },
            {
                dataField: 'amount',
                caption: 'Amount',
                dataType: 'number',
                allowSorting: false,
                allowFiltering: false,
                validationRules: [{
                    type: 'required',
                    message: 'Amount is required.',
                }],
            },
        ],
        editing: {
            mode: 'popup',
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true,
            popup: {
                title: 'Info Transaksi',
                showTitle: true,
                width: 700,
                height: 525,
            },
            form: {
                items: [
                    {
                        itemType: 'group',
                        colSpan: 2,
                        items: ['accountId', 'masterTransactionId', 'debitCreditStatus', 'amount']
                    }
                ],
            },
        }
    }).dxDataGrid("instance");
});