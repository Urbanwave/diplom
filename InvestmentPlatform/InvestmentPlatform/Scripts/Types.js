$(document).ready(function () {
    $.getJSON('/type/GetImplementationStatuses', null, function (data) {
        $.each(data, function () {
            $('#implementationStatus').append('<option value=' +
                this.Id + '>' + this.Name + '</option>');
        });

        var implementationStatusId = $('#ImplementationStatusId').val();
        if ((implementationStatusId != '') && (implementationStatusId != null) && (implementationStatusId != "0")) {
            $('#implementationStatus').val(implementationStatusId)
        }
    });

    $('#implementationStatus').change(function () {
        $('#ImplementationStatusId').val($('#implementationStatus').val());
    });

    $.getJSON('/type/GetCurrencies', null, function (data) {
        $.each(data, function () {
            $('#currency').append('<option value=' +
                this.Id + '>' + this.Name + '</option>');
        });

        var currencyId = $('#CurrencyId').val();
        if ((currencyId != '') && (currencyId != null) && (currencyId != "0")) {
            $('#currency').val(currencyId)
        }
    });

    $('#currency').change(function () {
        $('#CurrencyId').val($('#currency').val());
    });
});