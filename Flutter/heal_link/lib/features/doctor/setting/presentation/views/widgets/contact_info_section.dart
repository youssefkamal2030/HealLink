import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/core/widgets/custom_elevated_button.dart';
import 'package:heal_link/features/doctor/personal_information/presentation/views/widgets/info_field.dart';
import 'package:heal_link/generated/l10n.dart';

class ContactInfoSection extends StatelessWidget {
  const ContactInfoSection({super.key});

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: Column(
        spacing: 8,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          InfoField(
            label: S.of(context).phoneNumber,
            value: '012345678989',
            isEditable: true,
            img: AppImages.edit,
          ),
          InfoField(
            label: S.of(context).email,
            value: 'YousryAhmed123@gmail.com',
            isEditable: true,
            img: AppImages.edit,
          ),
          InfoField(
            label: S.of(context).address,
            value: 'Cairo,Egypt',
            isEditable: true,
            img: AppImages.edit,
          ),
          InfoField(
            label: S.of(context).two_factor_authentication,
            isEditable: true,
          ),
          InfoField(label: S.of(context).deactivate_account, isEditable: true),
          Spacer(),
          Padding(
            padding: const EdgeInsets.only(bottom: 49.98),
            child: CustomElevatedButton(
              backGroundColor: AppColors.kDeleteColor,
              text: S.of(context).delete_account,
              onPressed: () {
                // Handle delete account action
              },
            ),
          ),
        ],
      ),
    );
  }
}
