(function($) {
    // The validator function
    $.validator.addMethod('rangeDate',
        function(value, element, param) {

            var dateVal = parseDate(value, "g"); // Comes from the KendoDatePicker, use the Global Format
            var minDate;
            var maxDate;

            if (value.length < 1 && param.nullable)
                return true;

            if (param.minDate.length > 0) {
                minDate = parseDate(param.minDate, "g");
            } else {

                var minControl = $('#' + param.minSelector);
                if (minControl.exists()) {
                    minDate = parseDate(minControl.val(), "g");
                } else {
                    return false;
                }
            }

            if (param.maxDate.length > 0) {
                maxDate = parseDate(param.maxDate, "g");
            } else {

                var maxControl = $('#' + param.maxSelector);
                if (maxControl.exists()) {
                    maxDate = parseDate(maxControl.val(), "g");
                } else {
                    return false;
                }
            }

            return minDate <= dateVal && dateVal <= maxDate;
        });

    // The adapter to support ASP.NET MVC unobtrusive validation
    $.validator.unobtrusive.adapters.add('rangedate', ['minselector', 'maxselector', 'mindate', 'maxdate', 'nullable'],
        function(options) {

            var params = {
                minSelector: options.params.minselector,
                maxSelector: options.params.maxselector,
                minDate: options.params.mindate,
                maxDate: options.params.maxdate,
                nullable: options.params.nullable == "true"
            };

            options.rules['rangeDate'] = params;
            if (options.message) {
                options.messages['rangeDate'] = options.message;
            }
        });

    
    function parseDate(dateString, format) {
        
        if (format == "d") {// 6/15/2009
            var dateParts = dateString.split("/");

            if (dateParts.length != 3) {
                throw "Invalid date for format specified";
            }

            var month = dateParts[0];
            var day = dateParts[1];
            var year = dateParts[2];

            var date = new Date(year, day, month);

            return date;
        } else {
            throw "Unsupported date for " + format;
        }

    };
}(jQuery));