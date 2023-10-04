"use strict"
var gridInstance;
$(document).ready(function () {
    var antiForgeryToken = document.getElementsByName("__RequestVerificationToken")[0].value;
    gridInstance = $('#roleGrid').dxDataGrid({
        dataSource: DevExpress.data.AspNet.createStore({
            key: 'id',
            loadUrl: `/ReportPoint/Read`,
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
                caption: 'ID',
                width: 50
            },
            {
                dataField: 'accountId',
                caption: 'Name',
                allowSorting: false,
                allowFiltering: false,
                lookup: {
                    dataSource: window.$users,
                    valueExpr: 'id',
                    displayExpr: 'text',
                }
            },
            {
                dataField: 'points',
                caption: 'Total Points',
                allowEditing: false,
                allowSorting: false,
                allowFiltering: false,
            },
        ],
    }).dxDataGrid("instance");
});