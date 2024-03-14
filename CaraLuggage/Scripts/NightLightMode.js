//let darkModeLocalStorage = localStorage.getItem('isDarkMode');

//let isDarkMode = darkModeLocalStorage = 'true'; //Ban đầu sẽ là lightmode

//if (darkModeLocalStorage == null) {
//    isDarkMode = false;
//}

////Logo light mode
//const lightModeLogo = 'Content/assets/img/logo.png';
////Logo dark mode
//const darkModeLogo = 'Content/assets/img/logodarkmode.png';

////Hàm update logo
//function updateLogo() {
//    const logoImg = document.getElementById('logo-mode');
//    if (isDarkMode) {
//        logoImg.src = darkModeLogo;
//    } else {
//        logoImg.src = lightModeLogo;
//    }
//}

////Chuyển đổi chế độ và cập nhật giao diện
//function toogleMode() {
//    const body = document.body;
//    const modeSwitchIcon = document.getElementById('mode-switch').querySelector('i');

//    //Thực hiện chuyển đổi
//    isDarkMode = !isDarkMode;
//    body.classList.toggle('dark-mode');

//    //Thay đổi Icon
//    if (isDarkMode) {
//        modeSwitchIcon.classList.remove('fa-moon');
//        modeSwitchIcon.classList.add('fa-sun');
//    }
//    else {
//        modeSwitchIcon.classList.remove('fa-sun');
//        modeSwitchIcon.classList.add('fa-moon');
//    }

//    //Lưu trạng thái chế độ vào localStorage
//    localStorage.setItem('isDarkMode', isDarkMode);

//    updateLogo();
//}

////Sự kiện khi nhấp vào nút
//document.getElementById('mode-switch').addEventListener('click', function () {
//    toogleMode();
//});

////Kiểm tra nếu chế độ đã được lưu vào trong localStorage
//if (darkModeLocalStorage == 'true') {
//    toogleMode();
//}

// Hàm để cập nhật trạng thái dark mode vào localStorage
function updateDarkModeInLocalStorage(isDarkMode) {
    localStorage.setItem('isDarkMode', isDarkMode);
}

// Hàm để kiểm tra và cập nhật trạng thái dark mode/light mode và logo
function checkAndUpdateDarkModeAndLogo() {
    let darkModeLocalStorage = localStorage.getItem('isDarkMode');
    let isDarkMode = darkModeLocalStorage === 'true';

    // Đường dẫn tới ảnh cho light mode
    const lightModeLogo = 'Content/assets/img/logo.png';
    // Đường dẫn tới ảnh cho dark mode
    const darkModeLogo = 'Content/assets/img/logodarkmode.png';

    // Hàm cập nhật logo dựa trên trạng thái hiện tại
    function updateLogo() {
        const logoImg = document.getElementById('logo-mode');
        if (isDarkMode) {
            logoImg.src = darkModeLogo;
            localStorage.setItem('logoPath', darkModeLogo); // Lưu đường dẫn logo hiện tại vào LocalStorage

            document.getElementById('mode-switch').querySelector('i').classList.remove('fa-moon');
            document.getElementById('mode-switch').querySelector('i').classList.add('fa-sun');
        } else {
            logoImg.src = lightModeLogo;
            localStorage.setItem('logoPath', lightModeLogo); // Lưu đường dẫn logo hiện tại vào LocalStorage

            document.getElementById('mode-switch').querySelector('i').classList.remove('fa-sun');
            document.getElementById('mode-switch').querySelector('i').classList.add('fa-moon');
        }
    }

    // Cập nhật lớp CSS cho thẻ body dựa trên trạng thái hiện tại
    function updateBodyClass() {
        const body = document.body;
        if (isDarkMode) {
            body.classList.add('dark-mode');
        } else {
            body.classList.remove('dark-mode');
        }
    }

    updateLogo();
    updateBodyClass();
}

// Sự kiện khi nhấp vào nút chuyển đổi mode
document.getElementById('mode-switch').addEventListener('click', function () {
    let darkModeLocalStorage = localStorage.getItem('isDarkMode');
    let isDarkMode = darkModeLocalStorage === 'true';

    // Thực hiện chuyển đổi trạng thái
    isDarkMode = !isDarkMode;

    // Cập nhật trạng thái dark mode vào localStorage
    updateDarkModeInLocalStorage(isDarkMode);

    // Cập nhật giao diện
    checkAndUpdateDarkModeAndLogo();
});

// Gọi hàm kiểm tra và cập nhật trạng thái dark mode/light mode và logo mỗi khi trang được tải
document.addEventListener('DOMContentLoaded', function () {
    checkAndUpdateDarkModeAndLogo();
});