
window.addEventListener('DOMContentLoaded', event => {

    //Nhấn để ẩn hiện thanh navbar bên trái
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sidenav-toggled');
            localStorage.setItem('sidebar-toggle', document.body.classList.contains('sidenav-toggled'));
        });
    }

    //load giao diện bảng hóa đơn
    const datatablesInvoice = document.getElementById('datatablesInvoice');
    if (datatablesInvoice) {
        new simpleDatatables.DataTable(datatablesInvoice);
    }
    //load giao diện chi tiết bảng hóa đơn
    const datatablesInvoiceDetail = document.getElementById('datatablesInvoiceDetail');
    if (datatablesInvoiceDetail) {
        new simpleDatatables.DataTable(datatablesInvoiceDetail);
    }
    //load giao diện bảng khách hàng
    const datatablesCustomer = document.getElementById('datatablesCustomer');
    if (datatablesCustomer) {
        new simpleDatatables.DataTable(datatablesCustomer);
    }
    //load giao dien bảng loại đồ
    const datatablesTypesClothes = document.getElementById('datatablesTypesClothes');
    if (datatablesTypesClothes) {
        new simpleDatatables.DataTable(datatablesTypesClothes);
    }
    //load giao dien bảng sản phẩm
    const datatablesProduct = document.getElementById('datatablesProduct');
    if (datatablesProduct) {
        new simpleDatatables.DataTable(datatablesProduct);
    }
    //load giao dien bảng feedback
    const datatablesFeedback = document.getElementById('datatablesFeedback');
    if (datatablesFeedback) {
        new simpleDatatables.DataTable(datatablesFeedback);
    }


    //Load biểu đồ giới tính
    new Chart(document.getElementById("myPieChart"), sexStatistics);
    //load biểu đồ theo năm
    new Chart(document.getElementById("myAreaChart"), yearStatistics);
    //load biểu đồ theo tháng
    new Chart(document.getElementById("myBarChart"), monthlyStatistics);
   
    
});


//Bảng thống kê theo năm
var yearStatistics = {
    type: "line",
    data: {
        labels: ["2022", "2023", "2024", "2025", "2026"],
        datasets: [
            {
                label: 'Doanh thu năm (đồng)',
                backgroundColor: '#4eb09b',
                borderColor: '#4eb09b',
                data: [120, 32, 44, 22, 55],
            }
        ],
    },
};
//hết bảng thống kê theo năm



//bảng thống kê theo tháng
var monthlyStatistics = {
    type: "bar",
    data: {
        labels: ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"],
        datasets: [
            {
                label: 'Doanh thu tháng (đồng)',
                backgroundColor: '#6c7ee1',
                borderColor: '#6c7ee1',
                data: [5, 2, 12, 19, 3, 4, 110, 120, 32, 44, 22, 55],
            }
        ],
    },
};
//hết bảng thống kê theo tháng


//Bảng thống kê theo giới tính
var sexStatistics = {
    type: 'pie',
    data: {
        labels: ["Nam", "Nữ"],
        datasets: [
            {
                data: [22, 55],
                backgroundColor: ['#007bff', '#dc3545'],
            }
        ],
    },
};
// hết bảng thống kê theo giới tính
