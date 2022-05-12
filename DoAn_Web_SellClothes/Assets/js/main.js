//hiệu ứng Active các button trên nav
const list = document.querySelectorAll('.navbar__item .navbar__item-link');
function activeLink() {
    list.forEach((item) => item.classList.remove('active'));
    this.classList.add('active');
}
list.forEach((item) => item.addEventListener('click', activeLink));