// let btn = document.getElementById("button");

// btn.addEventListener('click', function(){
//     alert("Plovaso");
// });

const $detailsList = document.querySelectorAll(".filters-details");
$detailsList.forEach( ($details)=>{
    $details.querySelector('summary').addEventListener('click', expand)
});
function expand(){
    $detailsList.forEach(($details) => {
        $details.removeAttribute('open');
    });
}

/**Filters dropdown*/
const btn_filters = document.getElementById('filters-dropdown-btn');
let btn_filters_height = btn_filters.clientHeight;
const filters_dropdown_block = document.getElementById('filters-dropdown-block');
filters_dropdown_block.style.top = btn_filters_height + 'px';

let count = 0;
btn_filters.addEventListener('click', e => {
    count++;
    if(count%2 == 0) {
        filters_dropdown_block.style.display = 'none';
    } else {
        filters_dropdown_block.style.display = 'block';
    }
    console.log(count)
})
