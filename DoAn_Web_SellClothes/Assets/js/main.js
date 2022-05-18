//trạng thái nút ở thanh lọc sản phẩm theo loại
$('.men-btn').click(function () {
    $('.sidebar ul .men-show').toggleClass("show");
    $('.sidebar ul .first').toggleClass("rotate");
})
$('.women-btn').click(function () {
    $('.sidebar ul .women-show').toggleClass("show1");
    $('.sidebar ul .second').toggleClass("rotate");
})

$('.sidebar ul li').click(function () {
    $(this).addClass("active").siblings().removeClass("active");
})

//chuyển ảnh trong chi tiết sản phẩm
$(document).ready(function () {
    $('.list__img-small img').click(function () {
        $('.imgBox img').attr("src", $(this).attr("src"));
    })
})

