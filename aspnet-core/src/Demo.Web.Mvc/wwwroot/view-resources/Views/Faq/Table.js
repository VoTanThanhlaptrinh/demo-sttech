var app = app || {};
app.faq = app.faq || {};

app.faq.table = (function ($) {
    var _$table = $('#FaqTable');
    var _$modalContainer = $('#FaqModalContainer');
    var _$faqDataTable = null;

    function init() {
        _$faqDataTable = _$table.DataTable({
            serverSide: true,
            processing: true,
            searching: false,
            ordering: false,
            pageLength: 10,
            lengthChange: false,
            autoWidth: false,
            responsive: false,
            language: {
                emptyTable: '<div class="faq-empty-state"><i class="fas fa-question-circle empty-icon d-block"></i><div class="empty-text">Không tìm thấy câu hỏi thường gặp</div></div>',
                zeroRecords: '<div class="faq-empty-state"><i class="fas fa-search empty-icon d-block"></i><div class="empty-text">Không tìm thấy câu hỏi thường gặp</div><div class="empty-suggestion">Không có kết quả nào phù hợp với tiêu chí tìm kiếm. Hãy thử thay đổi từ khóa hoặc nhấn <b>Đặt lại</b>.</div></div>',
                info: 'Hiển thị _START_ - _END_ trong tổng số _TOTAL_ câu hỏi',
                infoEmpty: 'Hiển thị 0 câu hỏi',
                infoFiltered: '(lọc từ _MAX_ câu hỏi)',
                processing: '<div class="text-center py-3"><i class="fas fa-spinner fa-spin fa-2x text-primary"></i><div class="mt-2 text-muted">Đang tải dữ liệu...</div></div>',
                paginate: {
                    first: '<i class="fas fa-angle-double-left"></i>',
                    last: '<i class="fas fa-angle-double-right"></i>',
                    next: '<i class="fas fa-chevron-right"></i>',
                    previous: '<i class="fas fa-chevron-left"></i>'
                }
            },
            ajax: function (data, callback, settings) {
                var keyword = ($('#FaqSearchKeyword').val() || '').trim();
                var statusVal = $('#FaqStatusFilter').val();
                var status = statusVal !== '' ? parseInt(statusVal, 10) : null;

                var input = {
                    keyword: keyword,
                    status: status,
                    skipCount: data.start,
                    maxResultCount: data.length
                };

                abp.ui.setBusy(_$table.closest('.card'));

                app.faq.service.getAll(input)
                    .done(function (result) {
                        callback({
                            draw: data.draw,
                            recordsTotal: result.totalCount,
                            recordsFiltered: result.totalCount,
                            data: result.items
                        });
                    })
                    .fail(function () {
                        callback({
                            draw: data.draw,
                            recordsTotal: 0,
                            recordsFiltered: 0,
                            data: []
                        });
                    })
                    .always(function () {
                        abp.ui.clearBusy(_$table.closest('.card'));
                    });
            },
            columnDefs: [
                {
                    targets: 0,
                    data: null,
                    className: 'text-center align-middle font-weight-bold text-muted',
                    width: '60px',
                    render: function (data, type, row, meta) {
                        return meta.settings._iDisplayStart + meta.row + 1;
                    }
                },
                {
                    targets: 1,
                    data: 'question',
                    className: 'align-middle',
                    render: function (data, type, row) {
                        var safeQuestion = app.faq.helpers.htmlEncode(data);
                        return '<div class="faq-question-title">' + safeQuestion + '</div>';
                    }
                },
                {
                    targets: 2,
                    data: 'answer',
                    className: 'align-middle',
                    render: function (data, type, row) {
                        var safeAnswer = app.faq.helpers.htmlEncode(data);
                        return '<div class="faq-answer-clamped" title="' + safeAnswer + '">' + safeAnswer + '</div>';
                    }
                },
                {
                    targets: 3,
                    data: 'status',
                    className: 'text-center align-middle',
                    width: '140px',
                    render: function (data, type, row) {
                        if (data === 1) {
                            return '<span class="badge bg-success badge-success px-2 py-1"><i class="fas fa-check-circle me-1"></i> Công khai</span>';
                        } else {
                            return '<span class="badge bg-secondary badge-secondary px-2 py-1"><i class="fas fa-eye-slash me-1"></i> Đang ẩn</span>';
                        }
                    }
                },
                {
                    targets: 4,
                    data: null,
                    className: 'text-center align-middle faq-actions-dropdown',
                    width: '110px',
                    render: function (data, type, row) {
                        var isPublished = row.status === 1;
                        var toggleText = isPublished ? 'Chuyển sang Đang ẩn' : 'Chuyển sang Công khai';
                        var toggleIcon = isPublished ? 'fa-eye-slash' : 'fa-eye';
                        var targetStatus = isPublished ? 2 : 1;

                        return [
                            '<div class="dropdown">',
                            '  <button class="btn btn-dropdown-dots dropdown-toggle" type="button" data-bs-toggle="dropdown" data-toggle="dropdown" aria-expanded="false" title="Thao tác">',
                            '    <i class="fas fa-ellipsis-v"></i>',
                            '  </button>',
                            '  <div class="dropdown-menu dropdown-menu-end dropdown-menu-right">',
                            '    <a class="dropdown-item view-faq-btn" href="javascript:void(0);" data-id="' + row.id + '">',
                            '      <i class="fas fa-info-circle text-info"></i> Xem chi tiết',
                            '    </a>',
                            '    <a class="dropdown-item edit-faq-btn" href="javascript:void(0);" data-id="' + row.id + '">',
                            '      <i class="fas fa-edit text-primary"></i> Chỉnh sửa',
                            '    </a>',
                            '    <a class="dropdown-item toggle-status-btn" href="javascript:void(0);" data-id="' + row.id + '" data-target-status="' + targetStatus + '">',
                            '      <i class="fas ' + toggleIcon + ' text-warning"></i> ' + toggleText,
                            '    </a>',
                            '    <div class="dropdown-divider"></div>',
                            '    <a class="dropdown-item text-danger delete-faq-btn" href="javascript:void(0);" data-id="' + row.id + '">',
                            '      <i class="fas fa-trash text-danger"></i> Xóa',
                            '    </a>',
                            '  </div>',
                            '</div>'
                        ].join('');
                    }
                }
            ]
        });

        bindEvents();
    }

    function triggerSearch() {
        if (_$faqDataTable) {
            _$faqDataTable.page('first').draw('page');
        }
    }

    function bindEvents() {
        $('#SearchBtn').on('click', function (e) {
            e.preventDefault();
            triggerSearch();
        });

        $('#FaqSearchKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                e.preventDefault();
                triggerSearch();
            }
        });

        $('#FaqStatusFilter').on('change', function () {
            triggerSearch();
        });

        $('#FaqPageLength').on('change', function () {
            var newLength = parseInt($(this).val(), 10) || 10;
            _$faqDataTable.page.len(newLength).page('first').draw('page');
        });

        $('#ResetFilterBtn').on('click', function (e) {
            e.preventDefault();
            $('#FaqSearchKeyword').val('');
            $('#FaqStatusFilter').val('');
            $('#FaqPageLength').val('10');
            _$faqDataTable.page.len(10).page('first').draw('page');
        });

        // View FAQ Modal
        $(document).on('click', '.view-faq-btn', function (e) {
            e.preventDefault();
            var faqId = $(this).data('id');

            abp.ui.setBusy(_$modalContainer);

            abp.ajax({
                url: abp.appPath + 'Faq/ViewModal?faqId=' + faqId,
                type: 'GET',
                dataType: 'html',
                abpHandleError: false,
                success: function (content) {
                    _$modalContainer.html(content);
                    var $modal = $('#FaqViewModal');
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

        // Toggle Status
        $(document).on('click', '.toggle-status-btn', function (e) {
            e.preventDefault();
            var faqId = $(this).data('id');
            var targetStatus = parseInt($(this).data('target-status'), 10);
            var targetStatusText = targetStatus === 1 ? 'Công khai' : 'Đang ẩn';

            abp.ui.setBusy(_$table.closest('.card'));

            app.faq.service.get({ id: faqId })
                .done(function (faq) {
                    var updateInput = {
                        id: faq.id,
                        question: faq.question,
                        answer: faq.answer,
                        status: targetStatus
                    };

                    app.faq.service.update(updateInput)
                        .done(function () {
                            abp.notify.success('Chuyển trạng thái sang "' + targetStatusText + '" thành công.');
                            reloadTable(false);
                        })
                        .always(function () {
                            abp.ui.clearBusy(_$table.closest('.card'));
                        });
                })
                .fail(function () {
                    abp.ui.clearBusy(_$table.closest('.card'));
                });
        });

        // Delete FAQ
        $(document).on('click', '.delete-faq-btn', function (e) {
            e.preventDefault();
            var faqId = $(this).data('id');

            abp.message.confirm(
                'Bạn có chắc chắn muốn xóa câu hỏi thường gặp này không?',
                'Xác nhận xóa',
                function (isConfirmed) {
                    if (isConfirmed) {
                        var pageInfo = getPageInfo();
                        var isLastItemOnCurrentPage = (pageInfo.end - pageInfo.start === 1) && pageInfo.page > 0;

                        abp.ui.setBusy(_$table.closest('.card'));

                        app.faq.service.delete({ id: faqId })
                            .done(function () {
                                abp.notify.success('Xóa câu hỏi thường gặp thành công.');
                                if (isLastItemOnCurrentPage) {
                                    drawPage(pageInfo.page - 1);
                                } else {
                                    reloadTable(false);
                                }
                            })
                            .always(function () {
                                abp.ui.clearBusy(_$table.closest('.card'));
                            });
                    }
                }
            );
        });
    }

    function reloadTable(resetPaging) {
        if (_$faqDataTable) {
            _$faqDataTable.ajax.reload(null, resetPaging !== false);
        }
    }

    function drawPage(pageIndex) {
        if (_$faqDataTable) {
            _$faqDataTable.page(pageIndex).draw('page');
        }
    }

    function getPageInfo() {
        return _$faqDataTable ? _$faqDataTable.page.info() : null;
    }

    function getTableElement() {
        return _$table;
    }

    return {
        init: init,
        reloadTable: reloadTable,
        drawPage: drawPage,
        getPageInfo: getPageInfo,
        getTableElement: getTableElement
    };
})(jQuery);
