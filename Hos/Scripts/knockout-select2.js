
ko.bindingHandlers.select2 = {
    init: function (el, valueAccessor, allBindingsAccessor, viewModel) {
        ko.utils.domNodeDisposal.addDisposeCallback(el, function () {
            $(el).select2('destroy');
        });

        var allBindings = allBindingsAccessor(),
            select2 = ko.utils.unwrapObservable(allBindings.select2);

        $(el).select2(select2);
    },
    update: function (el, valueAccessor, allBindingsAccessor, viewModel) {
        var allBindings = allBindingsAccessor();

        if ("value" in allBindings) {
            if (allBindings.select2.multiple && allBindings.value().constructor != Array) {
                $(el).select2("val", allBindings.value().split(","));
            }
            else {
                $(el).select2("val", allBindings.value());
            }
        } else if ("selectedOptions" in allBindings) {
            var converted = [];
            var textAccessor = function (value) { return value; };
            if ("optionsText" in allBindings) {
                textAccessor = function (value) {
                    var valueAccessor = function (item) { return item; }
                    if ("optionsValue" in allBindings) {
                        valueAccessor = function (item) { return item[allBindings.optionsValue]; }
                    }
                    var items = $.grep(allBindings.options(), function (e) { return valueAccessor(e) == value });
                    if (items.length == 0 || items.length > 1) {
                        return "UNKNOWN";
                    }
                    return items[0][allBindings.optionsText];
                }
            }
            $.each(allBindings.selectedOptions(), function (key, value) {
                converted.push({ id: value, text: textAccessor(value) });
            });
            $(el).select2("data", converted);
        }
    }
};


// Knockout binding handlers for select2
// e.g. https://github.com/ivaynberg/select2/wiki/Knockout.js-Integration

/*ko.bindingHandlers["select2"] = {
    after: ["options", "value", "selectedOptions"],
    init: function (element, valueAccessor) {
        $(element).select2(ko.unwrap(valueAccessor()));

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).select2("destroy");
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        //trying various methods to register in interest in updating this, but these work with the options observable, not the individual value observables
        //var allBindings = allBindingsAccessor();
        //if (allBindings.options) { allBindings.options(); }
        //if (allBindings.value) { allBindings.value(); }
        //if (allBindings.selectedOptions) { allBindings.selectedOptions(); }
        $(element).trigger("change");
    }
};
(function () {
    var updateOptions = ko.bindingHandlers["options"]["update"];
    ko.bindingHandlers["options"]["update"] = function (element) {
        var ret = updateOptions.apply(null, arguments);
        var $el = $(element);
        if ($el.data("select2")) { $el.trigger("change"); }
        return ret;
    }
})();
(function () {
    var updateSelectedOptions = ko.bindingHandlers["selectedOptions"]["update"];
    ko.bindingHandlers["selectedOptions"]["update"] = function (element) {
        var ret = updateSelectedOptions.apply(null, arguments);
        var $el = $(element);
        if ($el.data("select2")) { $el.trigger("change"); }
        return ret;
    }
})();
*/