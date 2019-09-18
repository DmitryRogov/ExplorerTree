<template>
  <div class="folder-tree-container">
    <ul class="folder-tree">
      <FolderNode
        :data="data"
        :open="true"
        :selectedPath="value"
        @selected="selected"
      ></FolderNode>
    </ul>
  </div>
</template>
<script lang="ts">
import Vue from "vue";
import { Component, Prop } from "vue-property-decorator";
import TreeNode from "../models/TreeNode";

import FolderNode from "./FolderNode.vue";

@Component({ components: { FolderNode } })
export default class FolderTree extends Vue {
  @Prop({ type: Object, required: true })
  public data: TreeNode<string>;

  @Prop({ type: String, required: false })
  public value: string;

  private selected(path: string) {
    this.$emit("input", path);
  }
}
</script>
<style>
.folder-tree-container {
  overflow: auto;
  max-height: 480px;
}

.folder-tree:first-child {
  padding-left: 0px;
}

.folder-tree {
  list-style: none;
  padding-left: 24px;
}
</style>