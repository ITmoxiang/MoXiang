<template>
  <div class="about">
    <div class="site-content">
      <div class="content-warp">
        <div class="about-site about-info">
          <section-title>关于本人</section-title>
          <div class="info-card">
            <p>
              一个励志做架构师的boy，主攻方向.net相关技术。主要技能：<br />
              B/S：.net webform、.net
              core、ercore、dapper、vue、html、css、jquery <br />
              C/S：.net winform、wpf <br />
              数据库：mysql、sqlserver、orcle、redis<br />
              其他：docker、git、svn 等等。<br />
              坐标深圳，在职看机会。
            </p>
          </div>
        </div>
        <div class="about-site about-info">
          <section-title>关于博客</section-title>
          <div class="info-card">
            <p>
              本网站采用Vue+.net 5 项目开源地址：<a
                target="_blank"
                class="out-link"
                href="https://github.com/ITmoxiang/MoXiang"
                >GitHub</a>。
            </p>
          </div>
        </div>
        <div class="about-me about-info">
          <section-title id="Guestbook">给我留言</section-title>
          <div class="info-card">
            <div class="contactForm">
              <div class="form-item">
                <label for="mail">邮箱</label>
                <input
                  class="v"
                  type="email"
                  name="mail"
                  id="mail"
                  ref="email"
                />
              </div>
              <div class="form-item">
                <label for="content">内容</label>
                <textarea
                  class="v"
                  id="content"
                  name="content"
                  ref="content"
                ></textarea>
              </div>
              <div class="form-item">
                <label></label>
                <el-button type="primary" @click="run()">提交</el-button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import sectionTitle from "@/components/section-title";
//import {getTime,getTimeInterval} from '@/utils'
// import Quote from "@/components/quote";
// import {fetchFriend} from '../api'
import { addmessage } from "../api";
export default {
  name: "About",
  data() {
    return {
      list: [],
    };
  },
  components: {
    // Quote,
    sectionTitle,
  },
  methods: {
    run: function () {
      var regEmail = /^[A-Za-z0-9\u4e00-\u9fa5]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$/;
      if (
        this.$refs.email.value != "" &&
        !regEmail.test(this.$refs.email.value)
      ) {
        alert("邮箱错误，请输入正确的邮箱");
      }else{
        addmessage({
        content: this.$refs.content.value,
        eMail: this.$refs.email.value
      })
        .then((res) => {
          alert("留言成功，收到后会尽快回复您");
        })
        .catch((err) => {
          alert(err.Message)
        });
      }
      
    },
  },
  mounted() {},
};
</script>
<style scoped lang="less">
.about {
  padding-top: 40px;
}
.out-link {
  color: rgb(99, 235, 245);
}
.content-warp {
  margin-top: 80px;

  .about-info {
    margin: 30px 0;

    span {
      color: red;
      margin-right: 10px;
    }

    .info-card {
      min-height: 100px;
      padding: 20px;
      border-radius: 3px;
      margin: 30px 0 50px 0;
      box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.1);
      p {
        line-height: 1.7rem;
      }
    }
  }
  .contactForm {
    width: 100%;
    padding: 20px;
    .form-item {
      align-items: center;
      display: flex;
      &:not(:last-child) {
        margin-bottom: 20px;
      }
      label {
        width: 80px;
      }
      .v {
        min-height: 40px;
        line-height: 20px;
        border-radius: 3px;
        padding: 2px 10px;
        outline: none;
        border: 1px solid #8fd0cc;
        width: 100%;
        resize: vertical;
      }
    }
  }
}

/*******/
@media (max-width: 800px) {
  .content-warp {
    margin-top: 0;
  }
}
</style>
