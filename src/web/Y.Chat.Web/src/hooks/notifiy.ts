import { ElNotification } from 'element-plus'

interface NotifyOptions {
  duration?: number
  position?:string
}

interface INotifiy{

}

export function notify(options: NotifyOptions) {
  const { type, message, duration = 3000 } = options

}