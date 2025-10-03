
        // Tạo biến global để script có thể kiểm tra
        const isUserLoggedIn = @(User.Identity.IsAuthenticated ? "true" : "false");

        const resultsGrid = $('#results-grid');
        // Copy code JavaScript xử lý sự kiện click cho wishlist vào đây
        // Xử lý sự kiện click vào nút yêu thích
            resultsGrid.on('click', '.wishlist-toggle', function (e) {
                e.preventDefault(); // Ngăn hành vi mặc định của thẻ <a>

                // Kiểm tra xem người dùng đã đăng nhập chưa
                if (!isUserLoggedIn) {
                    // Mở popup đăng nhập nếu có, hoặc báo lỗi
                    alert('Vui lòng đăng nhập để sử dụng chức năng này.');
                    return; // Dừng lại
                }

                var button = $(this);
                var productId = button.data('product-id');
                var token = $('input[name="__RequestVerificationToken"]').val();

                $.ajax({
                    url: '/WishlistItem/Toggle', // Gọi đến WishlistController
                    type: 'POST',
                    data: {
                        productId: productId,
                        __RequestVerificationToken: token
                    },
                    success: function (response) {
                        if (response.success) {
                            // Tự động thêm/xóa class "active"
                            button.toggleClass('active', response.added);
                            $('#wishlist-count').text(response.count);
                        } else {
                            alert(response.message || 'Có lỗi xảy ra.');
                        }
                    },
                    error: function () {
                        alert('Đã có lỗi xảy ra khi xử lý yêu thích.');
                    }
                });
            });