var notyu = window.notyu || (function(document, $) {
    var _locale = 'zh-cn',
        _defaultLocale = 'zh-cn',
        _layout = 'center',
        that = {};

    that.alert = function (/*str, label, cb*/) {
        var str = "",
            label = _translate('OK'),
            type = "alert",
            cb = null;

        switch (arguments.length) {
            case 1:
                // no callback, default button label
                str = arguments[0];
                break;
            case 2:
                // callback *or* custom button label dependent on type
                str = arguments[0];
                type = arguments[1];
                break;
            case 3:
                // callback *or* custom button label dependent on type
                str = arguments[0];
                type = arguments[1];
                cb = arguments[2];
                break;
            default:
                throw new Error("Incorrect number of arguments: expected 1-3");
        }
        return that.dialog({
            layout: _layout,
            type: type,
            text: str,
            dismissQueue: true,
            modal: true,
            buttons: [
                {
                    addClass: 'btn btn-primary', text: label,
                    onClick:(cb != null) ? cb : function ($noty) {
                        $noty.close();
                    }
                }
            ]
        });
    }

    that.confirm = function (/*str, labelCancel, labelOk, cb*/) {
        var str = "",
            labelCancel = _translate('CANCEL'),
            labelOk = _translate('CONFIRM'),
            cb = null;

        switch (arguments.length) {
            case 1:
                str = arguments[0];
                break;
            case 2:
                str = arguments[0];
                cb = arguments[1];
                break;
            default:
                throw new Error("Incorrect number of arguments: expected 1-4");
        }

        var cancelCallback = function ($noty) {
            if (typeof cb === 'function') {
                $noty.close();
                return cb(false);
            } else {
                $noty.close();
            }
        };

        var confirmCallback = function ($noty) {
            if (typeof cb === 'function') {
                $noty.close();
                return cb(true);
            } else {
                $noty.close();
            }
        };

        return that.dialog({
            layout: _layout,
            type: 'confirm',
            text: str,
            dismissQueue: true,
            modal: true,
            buttons: [
                {
                    addClass: 'btn btn-primary', text: labelOk,
                    onClick: function ($noty) { return confirmCallback($noty); }
                },
                {
                    addClass: 'btn btn-danger', text: labelCancel,
                    onClick: function ($noty) { return cancelCallback($noty); }
                }
            ]
        });
    }

    that.tip = function (/*str, label, cb*/) {
        var str = "",
            type = "alert",
            cb = null;

        switch (arguments.length) {
            case 1:
                // no callback, default button label
                str = arguments[0];
                break;
            case 2:
                // callback *or* custom button label dependent on type
                str = arguments[0];
                type = arguments[1];
                break;
            case 3:
                // callback *or* custom button label dependent on type
                str = arguments[0];
                type = arguments[1];
                cb = arguments[2];
                break;
            default:
                throw new Error("Incorrect number of arguments: expected 1-3");
        }


        return that.dialog({
            layout: _layout,
            type: type,
            text: str,
            dismissQueue: true,
            timeout: 2000,
            callback: {
                onClose: cb
            }
        });

    }

    that.dialog = function (options) {
        noty(options);
    }
    //语言
    var _locales = {
        'en': {
            OK: 'OK',
            CANCEL: 'Cancel',
            CONFIRM: 'OK'
        },
        'fr': {
            OK: 'OK',
            CANCEL: 'Annuler',
            CONFIRM: 'D\'accord'
        },
        'de': {
            OK: 'OK',
            CANCEL: 'Abbrechen',
            CONFIRM: 'Akzeptieren'
        },
        'es': {
            OK: 'OK',
            CANCEL: 'Cancelar',
            CONFIRM: 'Aceptar'
        },
        'br': {
            OK: 'OK',
            CANCEL: 'Cancelar',
            CONFIRM: 'Sim'
        },
        'nl': {
            OK: 'OK',
            CANCEL: 'Annuleren',
            CONFIRM: 'Accepteren'
        },
        'ru': {
            OK: 'OK',
            CANCEL: 'Отмена',
            CONFIRM: 'Применить'
        },
        'it': {
            OK: 'OK',
            CANCEL: 'Annulla',
            CONFIRM: 'Conferma'
        },
        'zh-cn': {
            OK: '确定',
            CANCEL: '取消',
            CONFIRM: '确定'
        }
    };


    function _translate(str, locale) {
        // we assume if no target locale is probided then we should take it from current setting
        if (typeof locale === 'undefined') {
            locale = _locale;
        }
        if (typeof _locales[locale][str] === 'string') {
            return _locales[locale][str];
        }

        // if we couldn't find a lookup then try and fallback to a default translation

        if (locale != _defaultLocale) {
            return _translate(str, _defaultLocale);
        }

        // if we can't do anything then bail out with whatever string was passed in - last resort
        return str;
    }

    return that;

}(document, window.jQuery));
window.notyu = notyu;