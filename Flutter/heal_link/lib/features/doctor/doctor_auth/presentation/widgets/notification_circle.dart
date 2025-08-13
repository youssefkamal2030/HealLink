import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_router.dart';

import '../../../../../core/utils/app_images.dart';
import '../../../../../core/utils/function/app_colors.dart';

class NotificationCircle extends StatelessWidget {
  const NotificationCircle({super.key});

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: () {
       // context.push(AppRouter.doctorNotificationsView);
        context.push(AppRouter.doctorNotificationsView);
       
      },
      child: CircleAvatar(
        radius: 20.5,
        backgroundColor: AppColors.kWhiteColor,
        child: Stack(
          children: [
            SvgPicture.asset(AppImages.notification, fit: BoxFit.cover),
            Positioned(
              top: 2,
              right: 1,
              child: Container(
                height: 8,
                width: 8,
                decoration: BoxDecoration(
                  shape: BoxShape.circle,
                  
                  color: Color(0xffEF530E),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
