var app = app || {};
app.faq = app.faq || {};

app.faq.errorHandler = (function ($) {
    /**
     * Trích xuất message từ JSON object
     */
    function extractFromJson(json) {
        if (!json) return null;
        if (json.__abp && json.result) return json.result;
        if (json.error && json.error.message) return json.error.message;
        if (json.result) return json.result;
        if (json.message) return json.message;
        if (typeof json === 'string') return json;
        return null;
    }

    /**
     * Trích xuất thông điệp mặc định dựa vào HTTP status 4xx
     */
    function getClientErrorMessage(status) {
        switch (status) {
            case 404: return 'Dữ liệu không tồn tại hoặc đã bị xóa.';
            case 403: return 'Bạn không có quyền thực hiện thao tác này.';
            case 401: return 'Phiên làm việc đã hết hạn. Vui lòng đăng nhập lại.';
            case 400: return 'Yêu cầu không hợp lệ.';
            default: return 'Đã xảy ra lỗi khi xử lý yêu cầu.';
        }
    }

    /**
     * Hiển thị popup thông báo
     */
    function showAlert(result) {
        if (window.abp && abp.message && typeof abp.message.error === 'function') {
            abp.message.error(result.message, result.title);
        } else {
            alert(result.title + ': ' + result.message);
        }
    }

    /**
     * Trích xuất nội dung thông báo lỗi từ jqXHR
     */
    function extractMessage(jqXHR) {
        if (!jqXHR) return null;

        var message = extractFromJson(jqXHR.responseJSON);
        if (message) return message;

        if (jqXHR.responseText) {
            try {
                message = extractFromJson(JSON.parse(jqXHR.responseText));
                return message;
            } catch (e) {
                var trimmed = jqXHR.responseText.trim();
                if (trimmed && !trimmed.startsWith('<') && trimmed.length < 500) {
                    return trimmed;
                }
            }
        }

        if (jqXHR.statusText && jqXHR.statusText !== 'error' && jqXHR.statusText !== 'OK') {
            return jqXHR.statusText;
        }

        return null;
    }

    /**
     * Phân loại lỗi và hiển thị thông báo alert
     */
    function handle(jqXHR, fallbackMessage) {
        if (typeof fallbackMessage !== 'string' || ['error', 'timeout', 'abort', 'parsererror'].indexOf(fallbackMessage) !== -1) {
            fallbackMessage = null;
        }

        var status = jqXHR ? (jqXHR.status || 0) : 0;
        var extracted = extractMessage(jqXHR);

        var result = {
            status: status,
            title: 'Lỗi',
            message: extracted || fallbackMessage || 'Đã xảy ra lỗi không xác định.'
        };

        if (status === 0) {
            result.title = 'Lỗi kết nối';
            result.message = 'Không thể kết nối đến máy chủ. Vui lòng kiểm tra lại đường truyền mạng.';
            showAlert(result);
            return result;
        }

        if (status >= 500 && status < 600) {
            result.title = 'Lỗi hệ thống';
            result.message = 'Đã xảy ra sự cố từ phía hệ thống. Vui lòng thử lại sau hoặc liên hệ bộ phận hỗ trợ.';

            showAlert(result);
            return result;
        }

        if (status >= 400 && status < 500) {
            result.title = 'Lỗi';
            showAlert(result);
            return result;
        }

        // Fallback for other status codes
        showAlert(result);
        return result;
    }

    return {
        handle: handle,
        extractMessage: extractMessage
    };
})(jQuery);

// Alias tiện dụng cho toàn bộ app
app.errorHandler = app.faq.errorHandler;
