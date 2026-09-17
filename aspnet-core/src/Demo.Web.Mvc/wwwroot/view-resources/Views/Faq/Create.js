var app = app || {};
app.faq = app.faq || {};

app.faq.create = (function ($) {
    var _$modalContainer = $('#FaqModalContainer');

    function init() {
        bindEvents();
    }

    function bindEvents() {
        $('#CreateFaqBtn').on('click', function (e) {
            e.preventDefault();
            abp.ui.setBusy(_$modalContainer);

            abp.ajax({
                url: abp.appPath + 'Faq/CreateModal',
                type: 'GET',
                dataType: 'html',
                abpHandleError: false,
                success: function (content) {
                    _$modalContainer.html(content);
                    var $modal = $('#FaqCreateModal');
                    app.faq.helpers.initCharCounters($modal);
                    initCreateValidation($modal);
                    app.faq.helpers.showModal($modal);
                },
                error: function (jqXHR) {
                    app.faq.errorHandler.handle(jqXHR);
                },
                complete: function () {
                    abp.ui.clearBusy(_$modalContainer);
                }
            });
        });
    }

    function initCreateValidation($modal) {
        var $form = $modal.find('#FaqCreateForm');
        var $submitBtn = $modal.find('#SaveCreateFaqBtn');

        $form.on('submit', function (e) {
            e.preventDefault();

            // Custom client-side validation
            var question = ($form.find('#CreateQuestion').val() || '').trim();
            var answer = ($form.find('#CreateAnswer').val() || '').trim();
            var statusVal = $form.find('#CreateStatus').val();

            var isValid = true;
            $form.find('.error-msg').text('');
            $form.find('.form-control, .form-select').removeClass('is-invalid');

            if (!question) {
                $form.find('[data-field="Question"]').text('Câu hỏi là bắt buộc.');
                $form.find('#CreateQuestion').addClass('is-invalid');
                isValid = false;
            } else if (question.length > 500) {
                $form.find('[data-field="Question"]').text('Độ dài câu hỏi tối đa là 500 ký tự.');
                $form.find('#CreateQuestion').addClass('is-invalid');
                isValid = false;
            }

            if (!answer) {
                $form.find('[data-field="Answer"]').text('Câu trả lời là bắt buộc.');
                $form.find('#CreateAnswer').addClass('is-invalid');
                isValid = false;
            } else if (answer.length > 4000) {
                $form.find('[data-field="Answer"]').text('Độ dài câu trả lời tối đa là 4000 ký tự.');
                $form.find('#CreateAnswer').addClass('is-invalid');
                isValid = false;
            }

            if (!statusVal || (statusVal !== '1' && statusVal !== '2')) {
                $form.find('[data-field="Status"]').text('Trạng thái không hợp lệ.');
                $form.find('#CreateStatus').addClass('is-invalid');
                isValid = false;
            }

            if (!isValid) {
                return;
            }

            var input = {
                question: question,
                answer: answer,
                status: parseInt(statusVal, 10)
            };

            $submitBtn.prop('disabled', true).html('<i class="fas fa-spinner fa-spin me-1"></i> Đang lưu...');
            abp.ui.setBusy($modal.find('.modal-content'));

            app.faq.service.create(input)
                .done(function () {
                    app.faq.helpers.hideModal($modal);
                    abp.notify.success('Thêm câu hỏi thường gặp thành công.');

                    if (app.faq.table) {
                        app.faq.table.drawPage('first');
                    }
                })
                .always(function () {
                    $submitBtn.prop('disabled', false).html('<i class="fas fa-save me-1"></i> Lưu');
                    abp.ui.clearBusy($modal.find('.modal-content'));
                });
        });
    }

    return {
        init: init
    };
})(jQuery);
