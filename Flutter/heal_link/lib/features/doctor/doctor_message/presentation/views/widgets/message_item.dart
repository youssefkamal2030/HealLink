import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/custom_circle_image.dart';
import 'package:heal_link/features/doctor/doctor_message/data/models/message_model.dart';

class MessageItem extends StatelessWidget {
  final MessageModel message;

  const MessageItem({super.key, required this.message});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 50,
      child: ListTile(
        minTileHeight: 0,
        contentPadding: EdgeInsets.symmetric(horizontal: 0,vertical: 0),
        leading: CircleAvatar(
          radius: 25,
          child: CustomCircleImage(
            image: AppImages.person,
            isVerified: false,
            radius: 25,
          ),
        ),
        title: Text(
          message.name,
          style: AppTextStyles.popins500style14LightBlackColor,
        ),
        subtitle: Text(
          message.message,
          overflow: TextOverflow.ellipsis,
          style: AppTextStyles.popins400style12LightBlackColor.copyWith(
            color: AppColors.kDarkGreyColor,
          ),
        ),
        trailing: Column(
          crossAxisAlignment: CrossAxisAlignment.end,
          spacing: 4,
          children: [
            Text(
              message.time,
              style: AppTextStyles.popins500style14LightBlackColor.copyWith(
                color:
                    message.isRead
                        ? AppColors.kDarkGreyColor
                        : AppColors.kUnreadCountColor,
              ),
            ),
            if (!message.isRead)
              Container(
                width: 20,
                height: 20,
                alignment: Alignment.center,
                decoration: BoxDecoration(
                  shape: BoxShape.circle,
                  color: AppColors.kUnreadCountColor,
                ),
                child: Text(
                  message.unreadCount,
                  style: AppTextStyles.popins400style12LightBlackColor.copyWith(
                    color: AppColors.kWhiteColor,
                  ),
                ),
              ),
          ],
        ),
        onTap: () {},
      ),
    );
  }
}
