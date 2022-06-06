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

    const datatablesInvoiceDetail = document.getElementById('datatablesInvoiceDetail');
    if (datatablesInvoiceDetail) {
        new simpleDatatables.DataTable(datatablesInvoiceDetail);
    }

});




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

new Chart(document.getElementById("myBarChart"), monthlyStatistics)
//hết bảng thống kê theo tháng

//Bảng thống kê theo năm
var monthlyStatistics = {
    type: "line",
    data: {
        labels: ["2022", "2023", "2024", "2025", "2026"],
        datasets: [
            {
                label: 'Doanh thu năm (đồng)',
                backgroundColor: '#4eb09b',
                borderColor: '#4eb09b',
                data: [ 120, 32, 44, 22, 55],
            }
        ],
    },
};

new Chart(document.getElementById("myAreaChart"), monthlyStatistics)
//hết bảng thống kê theo năm
