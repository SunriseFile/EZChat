<template>
    <div class="main-block">
        <div id="all-messages-block">
            <div class="message" v-for="message in messages" :key="message.id">
                <div class="send-time">{{message.timeH}}:{{message.timeM}}</div>
                <div class="message-inner">
                    {{ message.content }}
                </div>
            </div>
        </div>
        <div id="message-input-block">
            <input class="messageInput" type="text" placeholder="Начните вводить сообщение..." v-model="newMessage" @keyup.enter="sendMessage">
        </div>
    </div>
</template>


<script>
    import MesType from './typeWatch.vue';
    export default {
        data() {
            return {
                newMessage: '',
                idForMessage: 1,
                messageType: '',
                currentTimeH: new Date().getHours(),
                currentTimeM: new Date().getMinutes(),
                messages: []
            }
        },
        methods: {
            sendMessage() {
                this.messages.push({
                    id: this.idForMessage,
                    content: this.newMessage,
                    timeH: this.currentTimeH,
                    timeM: this.currentTimeM
                })
                this.currentTimeH = new Date().getHours()
                this.currentTimeM = new Date().getMinutes()
                this.newMessage = ''
                this.idForMessage++
            }
        },
        components: {
            'messType': MesType
        }
    }
</script>

