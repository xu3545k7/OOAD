<template>
    <div>
        <h1>Login</h1>
        <input v-model="username" placeholder="Username" />
        <input v-model="password" type="password" placeholder="Password" />
        <button @click="login">Login</button>
    </div>
</template>

<script>
import { login } from "@/services/api";

export default {
    data() {
        return {
            username: "",
            password: "",
        };
    },
    methods: {
        async login() {
            try {
                const response = await login(this.username, this.password);
                alert(`Welcome ${response.data.username}`);
            } catch (error) {
                alert("Login failed");
            }
        },
    },
};
</script>
