
var paw = {

    onAdditionalData: function () {
        return {
            text: $("#searchAutoComplete").val()
        };
    },

    onAjaxFormSuccess: function (data) {
        if (data.Result === "Success") {
            if (data.hasOwnProperty("Url")) {
                location.href = data.Url;
            }
            else {
                location.reload(true);
            }
        }
    },

    onRedirect: function (data) {
        if (data.Result === "Success") {
            if (data.hasOwnProperty("Url")) {
                location.href = data.Url;
            }
            else {
                location.reload(true);
            }
        }
        else if (data.hasOwnProperty("Message")) {
            alert(data.Message);
        }
    },

    onRefresh: function (data) {
        if (data.Result === "Success") {
            location.reload(true);
        }
    },

    onSelect: function (e) {
        var dataItem = this.dataItem(e.item.index());
    },

    updateContainer: function (data) {
        $.ajax({
            type: data.action,
            url: data.url,
            data: data,
            context: data.container,
            success: function (result) {
                $(data.container).html(result);
            }
        });
    },

    util: {
        isEmpty: function (str) {
            return (!str || 0 === str.length);
        }
    },

    search: {
        onSelect: function (e) {
            if (e.item) {
                var dataItem = this.dataItem(e.item.index());

                var url = document.location.protocol + '//' +
                    document.location.host +
                    document.location.pathname +
                    '?OwnerId=' +
                    dataItem.OwnerId;

                console.log(url);

                location.href = url;
            }
            else {
                console.log("change");
            }
        }
    }
};

$('body').on('click', '[data-toggle="modal"]', function () {
    $('#commonDialogContent').load($(this).attr('href'));
});

$('.modal').on('hidden.bs.modal', function () {
    $('#commonDialogContent').val('');
});
