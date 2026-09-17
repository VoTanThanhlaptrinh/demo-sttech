var app = app || {};
app.faq = app.faq || {};

app.faq.edit = (function ($) {
    var _$modalContainer = $('#FaqModalContainer');

    function init() {
        bindEvents();
    }
    // Lắng nghe sự kiện click nút edit
    // gọi api lấy template và hiển thị modal
    function bindEvents() {
        $(document).on('click', '.edit-faq-btn', function (e) {
            e.preventDefault();
            var faqId = $(this).data('id');

            abp.ui.setBusy(_$modalContainer);

            abp.ajax({
                url: abp.appPath + 'Faq/EditModal?faqId=' + faqId,
                type: 'GET',
                dataType: 'html',
                abpHandleError: false,
                success: function (content) {
                    _$modalContainer.html(content);
                    var $modal = $('#FaqEditModal');
                    app.faq.helpers.initCharCounters($modal);
                    initEditValidation($modal);
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

    function initEditValidation($modal) {
        var $form = $modal.find('#FaqEditForm');
        var $submitBtn = $modal.find('#SaveEditFaqBtn');

        $form.on('submit', function (e) {
            e.preventDefault();

            var id = parseInt($form.find('input[name="Id"]').val(), 10);
            var question = ($form.find('#EditQuestion').val() || '').trim();
            var answer = ($form.find('#EditAnswer').val() || '').trim();
            var statusVal = $form.find('#EditStatus').val();

            var isValid = true;
            $form.find('.error-msg').text('');
            $form.find('.form-control, .form-select').removeClass('is-invalid');

            if (!question) {
                $form.find('[data-field="Question"]').text('Câu hỏi là bắt buộc.');
                $form.find('#EditQuestion').addClass('is-invalid');
                isValid = false;
            } else if (question.length > 500) {
                $form.find('[data-field="Question"]').text('Độ dài câu hỏi tối đa là 500 ký tự.');
                $form.find('#EditQuestion').addClass('is-invalid');
                isValid = false;
            }

            if (!answer) {
                $form.find('[data-field="Answer"]').text('Câu trả lời là bắt buộc.');
                $form.find('#EditAnswer').addClass('is-invalid');
                isValid = false;
            } else if (answer.length > 4000) {
                $form.find('[data-field="Answer"]').text('Độ dài câu trả lời tối đa là 4000 ký tự.');
                $form.find('#EditAnswer').addClass('is-invalid');
                isValid = false;
            }

            if (!statusVal || (statusVal !== '1' && statusVal !== '2')) {
                $form.find('[data-field="Status"]').text('Trạng thái không hợp lệ.');
                $form.find('#EditStatus').addClass('is-invalid');
                isValid = false;
            }

            if (!isValid) {
                return;
            }

            var input = {
                id: id,
                question: question,
                answer: answer,
                status: parseInt(statusVal, 10)
            };

            $submitBtn.prop('disabled', true).html('<i class="fas fa-spinner fa-spin me-1"></i> Đang lưu...');
            abp.ui.setBusy($modal.find('.modal-content'));

            app.faq.service.update(input)
                .done(function () {
                    app.faq.helpers.hideModal($modal);
                    abp.notify.success('Cập nhật câu hỏi thường gặp thành công.');
                    
                    if (app.faq.table) {
                        app.faq.table.reloadTable(false); // Keep current page
                    }
                })
                .always(function () {
                    $submitBtn.prop('disabled', false).html('<i class="fas fa-save me-1"></i> Lưu thay đổi');
                    abp.ui.clearBusy($modal.find('.modal-content'));
                });
        });
    }

    return {
        init: init
    };
})(jQuery);
