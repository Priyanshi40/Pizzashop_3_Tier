// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

// -------------------- NAVBAR -----------------------------------------
document.getElementById('sidebarToggle').addEventListener('click', function () {
    var sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('show');
    var content = document.getElementById('content');
    if (sidebar.classList.contains("show")) {
        content.classList.add("shifted");
    } else {
        content.classList.remove("shifted");
    }
});
document.getElementById('closeBtn').addEventListener('click', function () {
    var sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('show');
});


const passIcon = document.querySelector('#passwordIcon');
const pass = document.querySelector('#password');
passIcon.addEventListener('click',function(){
    
    const type = pass.getAttribute('type') === 'password' ? 'text' : 'password';
    pass.setAttribute('type',type);
    this.classList.toggle('fa-eye');
});

let btn = document.querySelector('#btnSubmit');
let email = document.querySelector('#email');
let eDiv = document.querySelector('#emailDiv');
let pDiv = document.querySelector('#passwordDiv');
let emailDiv = document.createElement('span');
let passwordDiv = document.createElement('span');


email.addEventListener('keypress',function(){
    console.log(email.value);
    if(email.value.length!=0){
        emailDiv.innerHTML="";
    }
});

pass.addEventListener('keypress',function(){
    console.log(pass.value);
    if(pass.value.length!=0){
        passwordDiv.innerHTML="";
    }
});

btn.addEventListener('click',function(){
    console.log("Button Clicked");
    if(email.value==""||email.value==null){
        eDiv.appendChild(emailDiv);
        emailDiv.innerHTML="Please enter email id !!";
        emailDiv.style.color="red";
    };
    if(pass.value==""||pass.value==null){
        pDiv.appendChild(passwordDiv);
        passwordDiv.innerHTML="Please enter Password !!";
        passwordDiv.style.color="red";
    };
});

// // let form = document.querySelector('#loginForm');
// // form.addEventListener('submit',function(){
// // function validation(){
//     console.log("Button Clicked");
//     if(email.value==""||email.value==null){
//         eDiv.appendChild(emailDiv);
//         emailDiv.innerHTML="Please enter email id !!";
//         emailDiv.style.color="red";
//     };
//     if(pass.value==""||pass.value==null){
//         pDiv.appendChild(passwordDiv);
//         passwordDiv.innerHTML="Please enter Password !!";
//         passwordDiv.style.color="red";
//     };
// // };
// // });

