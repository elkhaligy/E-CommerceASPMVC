// document.addEventListener('DOMContentLoaded', function() {
//     console.log("DOMContentLoaded");
//     const searchButton = document.getElementById('searchButton');
//     const searchInput = document.getElementById('searchInput');

//     searchButton.addEventListener('click', function() {
//         const searchTerm = searchInput.value;
        
//         fetch(`/Product/Search?searchTerm=${encodeURIComponent(searchTerm)}`)
//             .then(response => response.text())
//             .then(html => {
//                 document.open();
//                 document.write(html);
//                 document.close();
//             })
//             .catch(error => {
//                 console.error('Search failed:', error);
//             });
//     });
// });