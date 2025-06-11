import axios from "axios";

const state = {
    userName: "",
    role: "",
};

const getters = {
    isLoggedIn: (state) => !!state.userName,
};

const actions = {
    async fetchAccountData({ commit }) {
        try {
            const response = await axios.get("http://localhost:5164/api/account");
            commit("setAccountData", response.data);
        } catch (error) {
            console.error("Error fetching account data:", error);
        }
    },
    async login({ commit }, { username, password }) {
        try {
            const response = await axios.post("http://localhost:5164/api/account/login", {
                username,
                password,
            });
            commit("setAccountData", response.data);
        } catch (error) {
            console.error("Login failed:", error);
            throw error;
        }
    },
    async logout({ commit }) {
        try {
            await axios.post("http://localhost:5001/api/account/logout");
            commit("clearAccountData");
        } catch (error) {
            console.error("Logout failed:", error);
        }
    },
};

const mutations = {
    setAccountData(state, data) {
        state.userName = data.userName;
        state.role = data.role;
    },
    clearAccountData(state) {
        state.userName = "";
        state.role = "";
    },
};

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations,
};
