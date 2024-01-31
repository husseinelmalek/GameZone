$(document).ready(function () {
    $('#Cover').on('change', function () {
        $('.review').attr('src', window.URL.createObjectURL(this.files[0])).removeClass('d-none')
    });
});
// function handleFileInputChange() {
//     var fileinput = document.getElementById("fileinput");

//     document.getElementById("review").src = window.URL.createObjectURL(fileinput.files[0]);

// }
//setAttribute("src", window.URL.createObjectURL(this.files[0]));