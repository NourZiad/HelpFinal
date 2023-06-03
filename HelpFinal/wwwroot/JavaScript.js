/***************************************************************************
 *  																	                                     *
 *  Hello! Thank you for stopping by!	    							                   *
 *  Don't forget to follow me on: 										                     *
 *	Dribbble: https://dribbble.com/gardiandesign						               *
 *	LinkedIn: https://www.linkedin.com/in/darjan-gardinovacki-602252132/   *
 *  Cheers, Darjan! 										 			                             *
 * 																		                                     *
***************************************************************************/

function signUp() {
    let element = document.getElementById("container_SignIn");
    element.classList.add("myStyle");
}

function signIn() {
    let element = document.getElementById("container_SignIn");
    element.classList.remove("myStyle");
}




    function selectOption(option) {
                        var skillsField = document.getElementById('skillsField');
    var disabilityField = document.getElementById('disabilityField');

    if (option === 'volunteer') {
        skillsField.style.display = 'block';
    disabilityField.style.display = 'none';
                        } else if (option === 'disabled') {
        skillsField.style.display = 'none';
        disabilityField.style.display = 'block';


                        }
}
function readInputRequirement(option) {
    var input = event.target;
    if (option === 'disabled' && input.value === '') {
        var label = input.previousElementSibling;
        var inputRequirement = label.innerText;

        var speech = new SpeechSynthesisUtterance();
        speech.lang = "en-US";
        speech.text = "Please" + inputRequirement;
        speech.rate = 0.5;
        speechSynthesis.speak(speech);
    }
}

//function readInputRequirement() {
//    var input = event.target;
//    if (input.value === '' && document.getElementById('IsDisabled').checked) {
//        var label = input.previousElementSibling;
//        var inputRequirement = label.innerText;

//        var speech = new SpeechSynthesisUtterance();
//        speech.lang = "en-US";
//        speech.text = "Please " + inputRequirement;
//        speech.rate = 0.5; 
//        speechSynthesis.speak(speech);
//    }
//}

//function toggleFields() {
//    var userTypes = document.getElementsByName('UserType');
//    var selectedUserType = "";

//    for (var i = 0; i < userTypes.length; i++) {
//        if (userTypes[i].checked) {
//            selectedUserType = userTypes[i].value;
//            break;
//        }
//    }

//    var skillsField = document.getElementById('skillsField');
//    var disabilityField = document.getElementById('disabilityField');

//    if (selectedUserType === 'volunteer') {
//        skillsField.style.display = 'block';
//        disabilityField.style.display = 'none';
//    } else if (selectedUserType === 'disabled') {
//        skillsField.style.display = 'none';
//        disabilityField.style.display = 'block';
//    } else {
//        skillsField.style.display = 'none';
//        disabilityField.style.display = 'none';
//    }
//}

//function toggleFields() {
//    var userType = document.querySelector('input[name="UserType"]:checked');

//    if (userType) {
//        var userTypeValue = userType.value;
//        var skillsField = document.getElementById('skillsField');
//        var disabilityField = document.getElementById('disabilityField');

//        if (userTypeValue === 'volunteer') {
//            skillsField.style.display = 'block';
//            disabilityField.style.display = 'none';
//        } else if (userTypeValue === 'disabled') {
//            skillsField.style.display = 'none';
//            disabilityField.style.display = 'block';
//        } else {
//            skillsField.style.display = 'none';
//            disabilityField.style.display = 'none';
//        }
//    }
//}




//function toggleFields() {
//    var isVolunteer = document.querySelector('input[name="IsVolunteer"]').checked;
//    var isDisabled = document.querySelector('input[name="IsDisabled"]').checked;
//    var skillsField = document.getElementById('skillsField');
//    var disabilityField = document.getElementById('disabilityField');

//    if (isVolunteer) {
//        skillsField.style.display = 'block';
//        disabilityField.style.display = 'none';
//    } else if (isDisabled) {
//        skillsField.style.display = 'none';
//        disabilityField.style.display = 'block';
//    } else {
//        skillsField.style.display = 'none';
//        disabilityField.style.display = 'none';
//    }
//}


//function toggleFields() {
//    var userType = document.querySelector('input[name="UserType"]:checked').value;
//    var skillsField = document.getElementById('skillsField');
//    var disabilityField = document.getElementById('disabilityField');

//    if (userType === 'volunteer') {
//        skillsField.style.display = 'block';
//        disabilityField.style.display = 'none';
//    } else if (userType === 'disabled') {
//        skillsField.style.display = 'none';
//        disabilityField.style.display = 'block';
//    } else {
//        skillsField.style.display = 'none';
//        disabilityField.style.display = 'none';
//    }
//}
