$.fn.exists = function() {
    return this.length !== 0;
};

(function ($) {
    // The validator function
    $.validator.addMethod('rangeDate',
        function(value, element, param) {

            var dateVal = parseDate(value, "G"); // Comes from the KendoDatePicker, use the Global Format
            var minDate;
            var maxDate;

            if (value.length < 1 && param.nullable)
                return true;

            if (param.minDate.length > 0) {
                minDate = parseDate(param.minDate, "G");
            } else {

                var minControl = $('#' + param.minSelector);
                if (minControl.exists()) {
                    minDate = parseDate(minControl.val(), "G");
                } else {
                    return false;
                }
            }

            if (param.maxDate.length > 0) {
                maxDate = parseDate(param.maxDate, "G");
            } else {

                var maxControl = $('#' + param.maxSelector);
                if (maxControl.exists()) {
                    maxDate = parseDate(maxControl.val(), "G");
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
        
        if (format == "G") {// 6/15/2009 12:00:00 PM
            var dateParts = dateString.split("/");

            if (dateParts.length != 3) {
                throw "Invalid date for format specified";
            }

            var month = dateParts[0];
            var day = dateParts[1];
            var yearSplit = dateParts[2].split(" ");
            if (dateParts.length != 3) {
                throw "Invalid date for format specified";
            }
            var year = yearSplit[0];
            
            var timeSplit = yearSplit[1].split(":");
            if (dateParts.length != 3) {
                throw "Invalid date for format specified";
            }
            
            var hour = timeSplit[0];
            var minute = timeSplit[1];
            var second = timeSplit[2];

            var isAm = yearSplit[2] == "AM";
            
            var date = new Date(year, month-1, day, isAm ? hour : hour + 12, minute, second);

            return date;
        } else {
            throw "Unsupported date for " + format;
        }

    };
}(jQuery));

