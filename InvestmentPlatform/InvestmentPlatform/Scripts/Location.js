$(document).ready(function () {
    $('#uploadImage').click(function (e) {
        e.preventDefault();
        $('#file').click();
    });

    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#loadedImage').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $("#file").change(function () {
        readURL(this);
    });

    $.getJSON('/location/GetCountries', null, function (data) {
        $.each(data, function () {
            $('#country').append('<option value=' +
                this.Id + '>' + this.Name + '</option>');
        });

        var cityId = $('#CityId').val();
        if ((cityId != '') && (cityId != null) && (cityId != "0")) {
            $.getJSON('/location/GetCountryIdByCityId', { cityId: $('#CityId').val() }, function (data) {
                $('#country').val(data)

                var countryValue = $('#country').val();
                if ((countryValue != '') && (countryValue != null)) {
                    $.getJSON('/location/GetCities', { countryId: $('#country').val() }, function (data) {
                        if (data.length > 0) {
                            $('#city').prop("disabled", false);

                            $.each(data, function () {
                                $('#city').append('<option value=' +
                                    this.Id + '>' + this.Name + '</option>');
                            });

                            if ((cityId != '') && (cityId != null)) {
                                $('#city').val(cityId);
                            }
                        }
                    });
                };
            });
        };
    });

    $('#country').change(function () {
        $('#city option').remove();
        $('#city').append('<option value="" disabled selected>Select City</option>');
        $.getJSON('/location/GetCities', { countryId: $('#country').val() }, function (data) {
            if (data.length > 0) {
                $('#city').prop("disabled", false);

                $.each(data, function () {
                    $('#city').append('<option value=' +
                        this.Id + '>' + this.Name + '</option>');
                });
            }
        });
    });

    $('#city').change(function () {
        $('#CityId').val($('#city').val());
    });
});