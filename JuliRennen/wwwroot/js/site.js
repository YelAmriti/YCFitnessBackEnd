// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var imageSrc = $(".card-img").find('img').attr("src");
imageSrc = imageSrc.replace(';', '');
$(".card-img").attr("src", imageSrc);