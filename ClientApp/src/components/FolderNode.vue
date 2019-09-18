<template>
  <li>
    <div
      @click="toggle"
      class="folder-node"
      :class="['folder-node', selected ? 'selected':'']"
    >
    <!-- <i :class="[collapsed ? 'flaticon-folder' : 'flaticon-folder-2' ,'mr-2']"></i> -->
    <svg class="icon">

   
    <use v-if="collapsed" xlink:href="#folder-2" />
<use v-else xlink:href="#folder-1" />
 </svg>
    {{data.data}}</div>
    <ul
      class="folder-tree"
      v-show="!collapsed"
    >
      <FolderNode
        v-for="child in data.children"
        :data="child"
        :selectedPath="selectedPath"
        @selected="childSelected"
        :key="child.data"
      ></FolderNode>
    </ul>
  </li>

</template>
<script lang="ts">
import Vue from "vue";
import { Component, Prop } from "vue-property-decorator";
import TreeNode from "../models/TreeNode";

@Component
export default class FolderNode extends Vue {
  @Prop({ type: Object })
  public data: TreeNode<string>;

  @Prop({ type: String, required: false })
  public selectedPath: string;

  @Prop({ type: Boolean, required: false })
  public open: boolean;

  private collapsed: boolean = true;

  created() {
    this.collapsed = !this.open;
  }

  private toggle() {
    this.$emit("selected", this.data.data);
    this.collapsed = !this.collapsed;
  }

  private childSelected(value: string) {
    this.$emit("selected", value);
  }

  private get selected(): boolean {
    return this.data.data == this.selectedPath;
  }
}
</script>
<style>
.folder-node {
  cursor: pointer;
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow-x: hidden;
  padding: 2px 4px;
  border-radius: 4px;
  transition:background .2s;
}

.folder-node.selected {
  background: #606b77;
  color: white;
}

.folder-node.selected:hover {
  background: #545e68;
}

.folder-node:hover {
  background: #eeeeee;
}
</style>