<template>
    <header>
        <h1>Account Management System</h1>
        <nav>
            <router-link to="/login">Login</router-link>
            <router-link to="/register">Register</router-link>
            <router-link to="/dashboard">Dashboard</router-link>
        </nav>
    </header>
</template>

<script>
export default {};
</script>

<style>
header {
    background-color: #007bff;
    color: white;
    padding: 10px 20px;
    display: flex;
    justify-content: space-between;
    align-items: center;
}

nav a {
    color: white;
    margin: 0 10px;
    text-decoration: none;
}

nav a:hover {
    text-decoration: underline;
}
</style>
