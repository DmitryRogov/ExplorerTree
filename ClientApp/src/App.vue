<template>
  <div class="container mt-3">
    <div class="row">
      <div class="col">
        <div class="input-group mb-3">
          <input
            type="text"
            class="inner-icon-left"
            :class="{'form-control':true, 'is-invalid' : error}"
            v-model.trim="selectedFolder"
            placeholder="Укажите сетевой путь"
          >
          <div class="input-group-prepend folder-icon-button">
            <button
              class="btn btn-transparent  px-1 py-1"
              type="button"
              @click="showExplorerDialog"
            >
              <svg class="icon">
                <use xlink:href="#folder-2" />
              </svg>
            </button>
          </div>
          <div class="input-group-append">
            <button
              type="button"
              @click="loadFolderGraph"
              class="btn btn-dark"
            >Выполнить</button>
          </div>
          <div
            class="invalid-feedback"
            v-if="error"
          >{{error}}</div>
        </div>
      </div>
    </div>
    <div
      class="row text-center"
      v-if="graph"
    >
      <Graph :data="graph"></Graph>
    </div>
    <div
      class="modal"
      tabindex="-1"
      role="dialog"
      v-if="showExplorerModal"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header p-2 bg-secondary text-white">
            <i class="modal-title">{{selectedModalFolder || 'Выберите папку'}}</i>
          </div>
          <div class="modal-body">
            <Tree
              :data="tree"
              v-model="selectedModalFolder"
              v-if="tree"
            ></Tree>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-outline-secondary"
              @click="hideExplorerDialog"
            >Отменить</button>
            <button
              type="button"
              class="btn btn-secondary"
              @click="selectFolderPath"
            >Выбрать</button>

          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import TreeNode from "./models/TreeNode";
import NodeBase from "./models/NodeBase";
import Graph from "./components/Graph.vue";
import Tree from "./components/FolderTree.vue";
import axios from "axios";

@Component({
  components: { Graph, Tree }
})
export default class App extends Vue {
  private selectedFolder: string = null;
  private graph: TreeNode<NodeBase> = null;
  private tree: TreeNode<string> = null;
  private showExplorerModal: boolean = false;
  private selectedModalFolder: string = null;
  private error: string = null;

  private selectFolderPath() {
    this.hideExplorerDialog();
    this.selectedFolder = this.selectedModalFolder;
    this.selectedModalFolder = null;
  }

  private showExplorerDialog() {
    this.showExplorerModal = true;
    this.loadFolderTree();
  }

  private hideExplorerDialog() {
    this.showExplorerModal = false;
  }

  private loadFolderTree() {
    axios
      .get<TreeNode<string>>("api/explorer/tree", {
        params: { root: this.selectedFolder }
      })
      .then(response => {
        this.tree = response.data;
      })
      .catch(e => {});
  }

  private validateSelectedFolder() {
    this.error = null;
    if (!this.selectedFolder) {
      this.error = "Укажите каталог";
      return false;
    }

    if (!this.selectedFolder.startsWith("\\\\")) {
      this.error = "Неверный формат сетевой папки";
      return false;
    }

    return true;
  }

  private loadFolderGraph() {
    if (!this.validateSelectedFolder()) {
      return;
    }

    axios
      .get<TreeNode<NodeBase>>("api/explorer/graph", {
        params: { root: this.selectedFolder }
      })
      .then(response => {
        this.graph = response.data;
      })
      .catch(e => {
        this.error = e.message;
      });
  }
}
</script>
<style>
.modal {
  display: block !important;
  background: #cccccc79;
}

.folder-icon-button{
  z-index: 9;
  margin-left:-40px;
}

.inner-icon-left{
  padding-right:54px !important;
}
</style>