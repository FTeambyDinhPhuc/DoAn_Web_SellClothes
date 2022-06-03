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

//nút tăng giảm số lượng
const plus = document.querySelector('.plus'),
    minus = document.querySelector('.minus'),
    num = document.querySelector('.num');

let a = 1;

plus.addEventListener("click", () => {
    a++;
    a = (a < 10) ? "0" + a : a;
    num.innerText = a;
});

minus.addEventListener("click", () => {
    if (a > 1) {
        a--;
        a = (a < 10) ? "0" + a : a;
        num.innerText = a;
    }
});


// hiện thông báo khi thêm sản phẩm vào giỏ hàng
// Toast function
function toast({ title = "", message = "", type = "info", duration = 3000 }) {
    const main = document.getElementById("toast");
    if (main) {
        const toast = document.createElement("div");

        // Auto remove toast
        const autoRemoveId = setTimeout(function () {
            main.removeChild(toast);
        }, duration + 1000);

        // Remove toast when clicked
        toast.onclick = function (e) {
            if (e.target.closest(".toast__close")) {
                main.removeChild(toast);
                clearTimeout(autoRemoveId);
            }
        };

        const icons = {
            success: "fas fa-check-circle",
            info: "fas fa-info-circle",
            warning: "fas fa-exclamation-circle",
            error: "fas fa-exclamation-circle"
        };
        const icon = icons[type];
        const delay = (duration / 1000).toFixed(2);

        toast.classList.add("mytoast", `toast--${type}`);
        toast.style.animation = `slideInLeft ease .3s, fadeOut linear 1s ${delay}s forwards`;

        toast.innerHTML = `
                    <div class="toast__icon">
                        <i class="${icon}"></i>
                    </div>
                    <div class="toast__body">
                        <h3 class="toast__title">${title}</h3>
                        <p class="toast__msg">${message}</p>
                    </div>
                    <div class="toast__close">
                        <i class="fas fa-times"></i>
                    </div>
                `;
        main.appendChild(toast);
    }
}

//thêm giỏ hàng thành công
 function showSuccessAddToCart() {
    toast({
      title: "Thành công!",
      message: "Bạn đã thêm sản phẩm thành công vào giỏ hàng.",
      type: "success",
      duration: 3000
    });
}
//hết hiện thông báo khi thêm sản phẩm vào giỏ hàng




