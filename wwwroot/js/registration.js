<script>
    // Toggle visibility of login and signup forms
    document.getElementById("login-btn").addEventListener("click", function() {
        document.getElementById("form-container-login").classList.remove("hidden");
    document.getElementById("form-container-signup").classList.add("hidden");
    });

    document.getElementById("signup-btn").addEventListener("click", function() {
        document.getElementById("form-container-signup").classList.remove("hidden");
    document.getElementById("form-container-login").classList.add("hidden");
    });
</script>
