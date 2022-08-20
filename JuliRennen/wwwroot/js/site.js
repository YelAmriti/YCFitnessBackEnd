// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/* JS code for seeRoute page */

$(document).ready(function () {
    $('.card-img').each(function () {
        $('img').each(function () {
            $(this).attr('src', $(this).attr('src').replace('wwwroot', '..'));
        })
    })
});