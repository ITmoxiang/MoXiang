<template>
  <div class="mavonEditor">
    <no-ssr>
      <mavon-editor class="editor"/>
    </no-ssr>
  </div>
</template>

<script>
import { mavonEditor } from "mavon-editor";
import "mavon-editor/dist/css/index.css";
export default {
  name: "Markdown",
  components: {
    mavonEditor
  }
};
</script>
<style lang="less" scoped>
.mavonEditor{
  width: 1500px;
  margin: 0 auto;
  margin-top: 90px;
  height: 1000px;
}
.editor{
  height: 100%;
}
</style>