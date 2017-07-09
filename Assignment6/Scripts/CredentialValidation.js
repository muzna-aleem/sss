function ValidateEmail(email) {
    
    // var expr = /^([\w-\.]+)((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;4
    var expr = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return expr.test(email);
}
function validatePassword(password) {
    var expr = /^[a-zA-Z]{5,100}$/;
    return expr.test(password);
}