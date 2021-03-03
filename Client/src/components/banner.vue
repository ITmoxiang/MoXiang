<template>
  <div id="banner" :class="{ 'home-banner': isHome }">
    <template v-if="isHome">
      <!--博主信息-->
      <div class="focusinfo">
        <!-- 头像 -->
        <div class="header-tou">
          <router-link to="/About"><img :src="websiteInfo.icon" /></router-link>
        </div>
        <!-- 简介 -->
        <div class="header-info">
          <p>{{ websiteInfo.slogan }}</p>
        </div>
        <!-- 社交信息 -->
        <div class="top-social">
          <div
            v-for="item in websiteInfo.socials"
            :key="item.id"
            :title="item.title"
          >
            <a :href="item.href" target="_blank" :style="{ color: item.color }">
              <i class="iconfont" :class="item.icon"></i>
            </a>
          </div>
        </div>
      </div>
      <!--左右倾斜-->
      <div class="slant-left"></div>
      <div class="slant-right"></div>
      <ul class="bg-bubbles">
        <li v-for="i in 10" :key="i"></li>
      </ul>
    </template>
  </div>
</template>

<script>
export default {
  name: "banner",
  data() {
    return {
      websiteInfo: {},
    };
  },
  props: {
    src: {
      type: String,
      default: "https://s3.ax1x.com/2021/02/18/yRLSMV.png",
    },
    isHome: {
      type: [Boolean, String],
      default: false,
    },
  },
  created() {
    this.getWebSiteInfo();
  },
  methods: {
    getWebSiteInfo() {
      this.$store.dispatch("getSiteInfo").then((data) => {
        this.websiteInfo = data;
      });
    },
  },
};
</script>

<style scoped lang="less">
#banner {
  position: relative;
  margin-top: 80px;
  width: 100%;
  height: 500px;
  background: linear-gradient(to bottom right, #50a3a2, #53e3a6);
  .banner-img {
    width: inherit;
    height: inherit;
    background-position: center;
    background-size: cover;
    background-repeat: no-repeat;
    transition: all 0.2s linear;
    overflow: hidden;
    &:hover {
      transform: scale(1.1, 1.1);
      filter: contrast(130%);
    }
  }
  &.home-banner {
    height: 550px;
    .banner-img {
      background-position: center center;
      background-repeat: no-repeat;
      background-attachment: fixed;
      background-size: cover;
      z-index: -1;
      transition: unset;
      &:hover {
        transform: unset;
        filter: unset;
      }
    }
    .slant-left {
      content: "";
      position: absolute;
      width: 0;
      height: 0;
      border-bottom: 100px solid #fff;
      border-right: 800px solid transparent;
      left: 0;
      bottom: 0;
    }
    .slant-right {
      content: "";
      position: absolute;
      width: 0;
      height: 0;
      border-bottom: 100px solid #fff;
      border-left: 800px solid transparent;
      right: 0;
      bottom: 0;
    }
  }
}
.focusinfo {
  position: relative;
  max-width: 800px;
  padding: 0 10px;
  top: 40%;
  left: 50%;
  transform: translate(-50%, -50%);
  -webkit-transform: translate(-50%, -50%);
  text-align: center;
  img {
    width: 80px;
    height: auto;
    border-radius: 50%;
    border: 3px solid rgba(255, 255, 255, 0.3);
  }
  .header-info {
    width: 60%;
    font-size: 14px;
    color: #eaeadf;
    background: rgba(0, 0, 0, 0.66);
    padding: 20px 30px;
    margin: 30px auto 0 auto;
    font-family: miranafont, "Hiragino Sans GB", STXihei, "Microsoft YaHei",
      SimSun, sans-serif;
    letter-spacing: 1px;
    line-height: 30px;
  }
  .top-social {
    height: 32px;
    margin-top: 30px;
    margin-left: 10px;
    list-style: none;
    display: inline-block;
    font-family: miranafont, "Hiragino Sans GB", STXihei, "Microsoft YaHei",
      SimSun, sans-serif;
    div {
      float: left;
      margin-right: 10px;
      height: 32px;
      line-height: 32px;
      text-align: center;
      width: 32px;
      background: white;
      border-radius: 100%;
    }
  }
  z-index: 99;
}
@media (max-width: 960px) {
  #banner {
    height: 400px;
  }
}
@media (max-width: 800px) {
  #banner {
    display: none;
  }
}
.bg-bubbles {
  position: absolute;
  /* 使气泡背景充满整个屏幕； */
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  // 如果元素内容超出给定的宽度和高度，overflow 属性可以确定是否显示滚动条等行为；
  overflow: hidden;
  li {
    position: absolute;
    /* // bottom 的设置是为了营造出气泡从页面底部冒出的效果； */
    bottom: -160px;
    /* // 默认的气泡大小； */
    width: 40px;
    height: 40px;
    background-color: rgba(255, 255, 255, 0.15);
    list-style: none;
    /* // 使用自定义动画使气泡渐现、上升和翻滚； */
    animation: square 15s infinite;
    transition-timing-function: linear;
    /* // 分别设置每个气泡不同的位置、大小、透明度和速度，以显得有层次感； */
    &:nth-child(1) {
      left: 10%;
    }
    &:nth-child(2) {
      left: 20%;
      width: 90px;
      height: 90px;
      animation-delay: 2s;
      animation-duration: 7s;
    }
    &:nth-child(3) {
      left: 25%;
      animation-delay: 4s;
    }
    &:nth-child(4) {
      left: 40%;
      width: 60px;
      height: 60px;
      animation-duration: 8s;
      background-color: rgba(255, 255, 255, 0.3);
    }
    &:nth-child(5) {
      left: 70%;
    }
    &:nth-child(6) {
      left: 80%;
      width: 120px;
      height: 120px;
      animation-delay: 3s;
      background-color: rgba(255, 255, 255, 0.2);
    }
    &:nth-child(7) {
      left: 32%;
      width: 160px;
      height: 160px;
      animation-delay: 2s;
    }
    &:nth-child(8) {
      left: 55%;
      width: 20px;
      height: 20px;
      animation-delay: 4s;
      animation-duration: 15s;
    }
    &:nth-child(9) {
      left: 25%;
      width: 10px;
      height: 10px;
      animation-delay: 2s;
      animation-duration: 12s;
      background-color: rgba(255, 255, 255, 0.3);
    }
    &:nth-child(10) {
      left: 85%;
      width: 160px;
      height: 160px;
      animation-delay: 5s;
    }
  }
  // 自定义 square 动画；
  @keyframes square {
    0% {
      opacity: 0.5;
      transform: translateY(0px) rotate(45deg);
    }
    25% {
      opacity: 0.75;
      transform: translateY(-400px) rotate(90deg);
    }
    50% {
      opacity: 1;
      transform: translateY(-600px) rotate(135deg);
    }
    100% {
      opacity: 0;
      transform: translateY(-1000px) rotate(180deg);
    }
  }
  z-index: 0;
}
</style>
