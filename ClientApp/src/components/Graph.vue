<template>
  <div
    class="org-tree-container"
    v-if="data"
  >
    <div class="org-tree">
      <GraphNode :data="data" />
    </div>
  </div>
</template>

<script lang="ts">
import Vue, { VNode } from "vue";
import { Component, Prop } from "vue-property-decorator";
import GraphNode from "./GraphNode.vue";
import TreeNode from "../models/TreeNode";
import NodeBase from "../models/NodeBase";

@Component({
  components: { GraphNode }
})
export default class Graph extends Vue {
  @Prop({
    type: Object,
    required: true
  })
  data: TreeNode<NodeBase>;
}
</script>
<style>
.org-tree-container {
  display: inline-block;
  padding: 15px;
  background-color: #fff;
  overflow-x: auto;
  width:100%;
}

.org-tree {
  display: table;
  text-align: center;
  margin: 0 auto;
}

.org-tree:after,
.org-tree:before {
  content: "";
  display: table;
}

.org-tree:after {
  clear: both;
}

.org-tree-node,
.org-tree-node-children {
  position: relative;
  margin: 0;
  padding: 0;
  list-style-type: none;
}

.org-tree-node-children:after,
.org-tree-node-children:before,
.org-tree-node:after,
.org-tree-node:before {
  transition: all 0.35s;
}

.org-tree-node-label {
  position: relative;
  display: inline-block;
}

.org-tree-node-label .org-tree-node-label-inner {
  padding: 10px 15px;
  text-align: center;
  border-radius: 3px;
  box-shadow: 0 1px 5px rgba(0, 0, 0, 0.15);
}

.org-tree-node-label .lable-name {
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow-x: hidden;
  max-width: 140px;
  min-width: 100px;
}

.org-tree-node-label .node-info {
  font-size: 0.75rem;
  text-align: left;
}

.org-tree-node {
  padding-top: 20px;
  display: table-cell;
  vertical-align: top;
}

.org-tree-node.is-leaf {
  padding-left: 10px;
  padding-right: 10px;
}

.org-tree-node:after,
.org-tree-node:before {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 50%;
  height: 19px;
}

.org-tree-node:after {
  left: 50%;
  border-left: 1px solid #ddd;
}

.org-tree-node:not(:first-child):before,
.org-tree-node:not(:last-child):after {
  border-top: 1px solid #ddd;
}

.org-tree > .org-tree-node {
  padding-top: 0;
}

.org-tree > .org-tree-node:after {
  border-left: 0;
}

.org-tree-node-children {
  padding-top: 20px;
  display: table;
}

.org-tree-node-children:before {
  content: "";
  position: absolute;
  top: 0;
  left: 50%;
  width: 0;
  height: 20px;
  border-left: 1px solid #ddd;
}

.org-tree-node-children:after {
  content: "";
  display: table;
  clear: both;
}
</style>