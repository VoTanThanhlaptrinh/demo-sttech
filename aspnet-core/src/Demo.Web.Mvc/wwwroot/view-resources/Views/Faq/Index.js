var app = app || {};
app.faq = app.faq || {};

(function ($) {
    app.faq.service = abp.services.app.faq;
    
    // Helpers
    app.faq.helpers = {
        htmlEncode: function (value) {
            return $('<div>').text(value || '').html();
        },
        showModal: function ($modal) {
            if (window.bootstrap && window.bootstrap.Modal) {
                var modalInstance = window.bootstrap.Modal.getOrCreateInstance($modal[0]);
                modalInstance.show();
            } else {
                $modal.modal('show');
            }
        },
        hideModal: function ($modal) {
            if (window.bootstrap && window.bootstrap.Modal) {
                var modalInstance = window.bootstrap.Modal.getInstance($modal[0]);
                if (modalInstance) {
                    modalInstance.hide();
                } else {
                    $modal.modal('hide');
                }
            } else {
                $modal.modal('hide');
            }
        },
        initCharCounters: function ($container) {
            $container.find('.char-counter').each(function () {
                var $counter = $(this);
                var targetId = $counter.data('for');
                var $input = $('#' + targetId);
                if ($input.length) {
                    var max = $input.attr('maxlength');
                    function update() {
                        var len = $input.val() ? $input.val().length : 0;
                        $counter.text(len + '/' + max);
                    }
                    $input.on('input propertychange', update);
                    update();
                }
            });
        },
        extractErrorMessage: function (jqXHR, fallbackMessage) {
            if (app.faq.errorHandler && typeof app.faq.errorHandler.extractMessage === 'function') {
                return app.faq.errorHandler.extractMessage(jqXHR) || fallbackMessage || 'Đã xảy ra lỗi.';
            }
            return fallbackMessage || 'Đã xảy ra lỗi.';
        }
    };

    $(document).ready(function () {
        if (app.faq.table && typeof app.faq.table.init === 'function') {
            app.faq.table.init();
        }
        if (app.faq.create && typeof app.faq.create.init === 'function') {
            app.faq.create.init();
        }
        if (app.faq.edit && typeof app.faq.edit.init === 'function') {
            app.faq.edit.init();
        }
    });
})(jQuery);
