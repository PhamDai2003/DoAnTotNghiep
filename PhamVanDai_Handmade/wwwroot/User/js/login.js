

// Đảm bảo toàn bộ code logic của bạn nằm trong sự kiện này
document.addEventListener('DOMContentLoaded', function () {

    // --- Lấy các phần tử ---
    const overlay = document.getElementById('overlay');
    const modalBox = document.getElementById('modalBox');
    const closeBtn = document.getElementById('closeBtn');
    const formTitle = document.getElementById('formTitle');
    const loginForm = document.getElementById('loginForm');
    const registerForm = document.getElementById('registerForm');
    const registerErrorDiv = document.getElementById('registerErrorMessage');
    const swapText = document.getElementById('swapText');
    const loginErrorDiv = document.getElementById('loginErrorMessage');

    if (!loginForm) {
        console.error('Không tìm thấy form đăng nhập!');
        return; // Dừng lại nếu không tìm thấy form
    }

    let isLogin = true;

    function renderForm() {
        // ... hàm renderForm của bạn giữ nguyên ...
        if (isLogin) {
            formTitle.textContent = "Đăng nhập";
            loginForm.style.display = "block";
            registerForm.style.display = "none";
            swapText.innerHTML = `Chưa có tài khoản? <a href="#" id="swapLink">Đăng ký</a>`;
        } else {
            formTitle.textContent = "Đăng ký";
            loginForm.style.display = "none";
            registerForm.style.display = "block";
            swapText.innerHTML = `Đã có tài khoản? <a href="#" id="swapLink">Đăng nhập</a>`;
        }

        document.getElementById('swapLink').addEventListener('click', e => {
            e.preventDefault();
            modalBox.classList.toggle('swap'); // hiệu ứng panel
            isLogin = !isLogin;
            setTimeout(renderForm, 400); // delay khớp animation
        });
    }

    // Mở modal
    window.openLogin = function () { // Gán vào window để HTML bên ngoài có thể gọi
        overlay.classList.add('active');
        isLogin = true;
        if (modalBox) modalBox.classList.remove('swap');
        renderForm();
        if (loginErrorDiv) loginErrorDiv.textContent = ''; // Xóa lỗi cũ
    }

    // Đóng modal
    if (closeBtn) {
        closeBtn.addEventListener('click', () => overlay.classList.remove('active'));
    }
    if (overlay) {
        overlay.addEventListener('click', e => {
            if (e.target === overlay) overlay.classList.remove('active');
        });
    }

    // --- LOGIC AJAX CHO FORM ĐĂNG NHẬP ---
    loginForm.addEventListener('submit', function (event) {
        event.preventDefault();

        const formData = new FormData(loginForm);

        fetch('/Account/Login', {
            method: 'POST',
            body: formData,
            headers: {
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            }
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    // Thành công: Tải lại trang để header được cập nhật đúng
                    window.location.reload();
                } else {
                    // Thất bại: Hiển thị lỗi
                    if (loginErrorDiv) {
                        loginErrorDiv.textContent = data.message;
                    }
                }
            })
            .catch(error => {
                console.error('Error:', error);
                if (loginErrorDiv) {
                    loginErrorDiv.textContent = 'Có lỗi xảy ra, vui lòng thử lại.';
                }
            });
    });

    // --- LOGIC AJAX CHO FORM ĐĂNG KÝ ---

    if (registerForm) {
        registerForm.addEventListener('submit', function (event) {
            event.preventDefault();

            const formData = new FormData(registerForm);

            fetch('/Account/Register', {
                method: 'POST',
                body: formData,
                headers: {
                    'RequestVerificationToken': document.querySelector('#registerForm input[name="__RequestVerificationToken"]').value
                }
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        // Đăng ký và đăng nhập thành công, tải lại trang
                        // để server render đúng trạng thái header "Chào bạn..."
                        window.location.reload();
                    } else {
                        // Đăng ký thất bại, hiển thị lỗi
                        if (registerErrorDiv) {
                            registerErrorDiv.textContent = data.message;
                        }
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                    if (registerErrorDiv) {
                        registerErrorDiv.textContent = 'Có lỗi xảy ra, vui lòng thử lại.';
                    }
                });
        });
    }

    // Khởi tạo form lần đầu
    renderForm();
});
