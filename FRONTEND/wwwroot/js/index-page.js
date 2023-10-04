"use strict"
$(document).ready(function () {
    $('#oneGrid').dxDataGrid({
        dataSource: DevExpress.data.AspNet.createStore({
            key: 'id',
            loadUrl: `/Read`,
            insertUrl: `/Create`,
            updateUrl: `/Edit`,
            deleteUrl: `/Delete`,
            onBeforeSend(method, ajaxOptions) {
                let antiForgeryToken = document.getElementsByName("__RequestVerificationToken")[0].value;
                if (antiForgeryToken) {
                    ajaxOptions.headers = {
                        "RequestVerificationToken": antiForgeryToken
                    };
                };
            },
        }),
        rowAlternationEnabled: true,
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
                dataField: 'id',
                caption: 'ID',
                width: 50
            },
            {
                dataField: 'name',
                caption: 'Account Name',
                validationRules: [{
                    type: 'required',
                    message: 'Name harus diisi.',
                }]
            },
        ],
        editing: {
            mode: 'popup',
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true,
            popup: {
                title: 'Account Info',
                showTitle: true,
                width: 700,
                height: 525,
            },
            form: {
                items: [
                    {
                        itemType: 'group',
                        colSpan: 2,
                        items: ['name']
                    }
                ],
            },
        },
    });
});